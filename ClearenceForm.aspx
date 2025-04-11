<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClearenceForm.aspx.cs" Inherits="ClearenceForm" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style type="text/css">
        .info_label {
            font-weight: bold;
            padding-left: 10px;
        }

        .info_group {
            display: flex;
            margin-bottom: 10px;
            padding: 10px;
            background: white;
            border-radius: 5px;
            width: 50%;
            text-align: right;
        }

        .LabelWhite {
            font-size: 18px;
            color: #ffff00;
            margin-bottom: 5px;
            margin-left: 2em;
        }

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

        /* Form Groups */
        .form_group {
            display: flex;
            flex-direction: column;
            margin-bottom: 15px;
        }

            .form_group label {
                font-weight: bold;
                color: yellow;
                margin-bottom: 5px;
            }

        /* Center Align */
        .center {
            text-align: center;
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

        .form_container {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 15px;
            max-width: 1000px;
            margin: auto;
        }

        .button-container {
            display: flex;
            align-items: center;
            justify-content: flex-end; /* Aligns all items to the right */
            gap: 10px; /* Adjusts spacing between elements */
        }

            .button-container .button {
                margin: 0; /* Removes any default margin around the buttons */
            }

            .button-container asp\:CheckBox {
                margin-right: 10px; /* Adjusts the space between the checkbox and the buttons */
            }

        .form_input input[type="radio"] {
            margin-right: 5px; /* Adjusts spacing between radio button and label */
        }

        .form_input label {
            margin-right: 10px; /* Adjusts spacing between Yes and No */
            display: inline-block;
            color: #fff;
        }
    </style>
    <script type="text/javascript"> 

        $(document).ready(document_Ready);

        function document_Ready() {
            /*Date Picker*/
            $(function () {
                $('.datepicker').datepicker({
                    dateFormat: "dd/mm/yy"//,
                    //minDate: -7
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
                <div class="body_content fullrow">
                    <%--Resigned Employee Listing Start--%>
                    <div class="panel" id="div_leaveRequests" runat="server">
                        <div class="panel_header">Clearance Form Listing</div>
                        <!--end panel_head-->
                        <div class="panel_body">
                            <!--Paste Grid code here-->
                            <asp:GridView ID="gvResignation" runat="server" Width="100%" AutoGenerateColumns="False"
                                HorizontalAlign="Center" AllowPaging="True" AllowSorting="True"
                                PageSize="1000" EmptyDataText="No Data Exist!" OnRowCommand="gvResignation_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="EmployeeCode" SortExpression="Status" HeaderText="Employee Code">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" SortExpression="Status" HeaderText="Employee Name">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DesigName" SortExpression="Status" HeaderText="Designation">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubmissionDate" SortExpression="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LastWorkingDate" SortExpression="LastWorkingDate" HeaderText="Last Working Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                        HtmlEncode="False">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NoticePeriod" SortExpression="NoticePeriod" HeaderText="NoticePeriod">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkClearance" runat="server"
                                                CommandName="Clearance" CommandArgument='<%# Container.DataItemIndex %>'>Clearance</asp:LinkButton>
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
            <%--Resigned Employee Information Start--%>
            <div class="outer_wrap" id="divREI" runat="server">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Resigned Employee Information</div>
                                    <div class="info_group">
                                        <label for="lblEmployeeCode">Employee Code:</label>
                                        <asp:Label ID="lblEmployeeCode" runat="server" class="info_label"></asp:Label>
                                    </div>
                                    <div class="info_group">
                                        <label for="lblEmployeeName">Employee Name:</label>
                                        <asp:Label ID="lblEmployeeName" runat="server" class="info_label"></asp:Label>
                                    </div>
                                    <div class="info_group">
                                        <label for="lblEmployeeDesignation">Designation:</label>
                                        <asp:Label ID="lblEmployeeDesignation" runat="server" class="info_label"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Resigned Employee Information End--%>

            <%--Accounts Departmen Startt--%>

            <div class="outer_wrap" runat="server" id="divAccounts">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Accounts Department</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label>Loan & Advances:</label>
                                            <asp:TextBox ID="txtLoadAdvances" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                        <div class="form_group">
                                            <label>Amount (If any):</label>
                                            <asp:TextBox ID="txtAccountAmount" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Fee Concession:</label>
                                        <div class="panel_body">
                                            <asp:GridView ID="gvFeeConcession" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                                HorizontalAlign="Center" PageSize="1000" EmptyDataText="No Data Exist!">
                                                <Columns>
                                                    <asp:BoundField DataField="CHILDREN_FEE_DEDUCTION" SortExpression="CHILDREN_FEE_DEDUCTION" HeaderText="CHILDREN_FEE_DEDUCTION" />
                                                    <%--<asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Roll No.">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Bind("RollNo") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBranch" runat="server" Text='<%# Bind("Branch") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Class" runat="server" Text='<%# Bind("Class") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtAccountRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span19" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span20" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span21" runat="server"></span></div>
                                        </div>
                                    </div>
                                    <div class="button-container">
                                        <asp:CheckBox ID="chkAccount" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveAccount" Width="100px" runat="server" Text="Save" OnClick="btnSaveAccount_Click" CssClass="button" />
                                        <asp:Button ID="btnUpdateAccount" Width="100px" runat="server" Text="Update" OnClick="btnUSaveAccount_Click" CssClass="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Accounts Departmen End--%>

            <%--IT Department Start--%>
            <div class="outer_wrap" runat="server" id="divIt">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">IT Department</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtItEquipment">IT Equipment:</label>
                                            <asp:TextBox ID="txtItEquipment" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>
                                        <div class="form_group">
                                            <label>Amount (If any):</label>
                                            <asp:TextBox ID="txtItAmount" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtLoginEmail">User Login & Email Disabled:</label>
                                            <%--<asp:TextBox ID="txtLoginEmail" Width="300px" runat="server" class="form_input" ></asp:TextBox>--%>
                                            <asp:RadioButtonList ID="rblLoginEmail" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtErpLogin">ERP Login Disabled:</label>
                                            <%--<asp:TextBox ID="txtErpLogin" Width="300px" runat="server" class="form_input" ></asp:TextBox>--%>
                                            <asp:RadioButtonList ID="rblLoginErp" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtItRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span16" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span17" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span18" runat="server"></span></div>
                                        </div>
                                    </div>

                                    <div class="button-container">
                                        <asp:CheckBox ID="chkIt" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveIt" Width="100px" runat="server" Text="Save" OnClick="btnSaveIt_Click" class="button" />
                                        <asp:Button ID="btnUpdateIt" Width="100px" runat="server" Text="Update" OnClick="btnUSaveIt_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--IT Department End--%>

            <%--Human Resource Start--%>
            <div class="outer_wrap" runat="server" id="divHr">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Human Resource</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtNoticePeriod">Notice Period Served:</label>
                                            <asp:TextBox ID="txtNoticePeriod" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <div class="form_group">
                                            <label for="txtLeavesDuringNoticePeriod">Leaves During Notice Period:</label>
                                            <asp:TextBox ID="txtLeavesDuringNoticePeriod" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <div class="form_group">
                                            <label for="txtHcDeduction">HC Deduction:</label>
                                            <asp:TextBox ID="txtHcDeduction" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <div class="form_group">
                                            <label>Amount (If any):</label>
                                            <asp:TextBox ID="txtHrAmount" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtExitInterview">Exit Interview Conducted:</label>
                                            <%--<asp:TextBox ID="txtExitInterview" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblHrExitInterviewConducted" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtStopSalary">Stop Salary Payment:</label>
                                            <%--<asp:TextBox ID="txtStopSalary" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblHrStopSalaryPayment" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtRehireCall">Rehire Call:</label>
                                            <%--<asp:TextBox ID="txtRehireCall" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblHrRehireCall" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label>In Case of No, Specify Reason:</label>
                                            <asp:TextBox ID="txtHrReason" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtHrRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span13" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span14" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span15" runat="server"></span></div>
                                        </div>
                                    </div>
                                    <div class="button-container">
                                        <asp:CheckBox ID="chkHr" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveHr" Width="100px" runat="server" Text="Save" OnClick="btnSaveHr_Click" class="button" />
                                        <asp:Button ID="btnUpdateHr" Width="100px" runat="server" Text="Update" OnClick="btnUSaveHr_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Human Resource End--%>

            <%--Library/TRC Start--%>
            <div class="outer_wrap" runat="server" id="divLib">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Library/TRC</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtResourcesCheckedOut">Resources Checked Out:</label>
                                            <asp:TextBox ID="txtResourcesCheckedOut" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>

                                        <div class="form_group">
                                            <label for="txtValueOfAssets">Value of Assets Not Returned:</label>
                                            <asp:TextBox ID="txtValueOfAssets" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>
                                        <div class="form_group">
                                            <label>Amount (If any):</label>
                                            <asp:TextBox ID="txtLibAmount" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtLibRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span10" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span11" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span12" runat="server"></span></div>
                                        </div>
                                    </div>

                                    <div class="button-container">
                                        <asp:CheckBox ID="chkLibrary" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveLibrary" Width="100px" runat="server" Text="Save" OnClick="btnSaveLibrary_Click" class="button" />
                                        <asp:Button ID="btnUpdateLibrary" Width="100px" runat="server" Text="Update" OnClick="btnUSaveLibrary_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Library/TRC End--%>

            <%--Admin (Cafeteria, Company Car, Guest House) Start--%>
            <div class="outer_wrap" runat="server" id="divAdmin">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Admin (Cafeteria, Company Car, Guest House)</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtMonth">Month:</label>
                                            <asp:TextBox ID="txtMonth" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>

                                        <div class="form_group">
                                            <label for="txtAmountDue">Amount Due:</label>
                                            <asp:TextBox ID="txtAmountDue" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtAdminRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span7" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span8" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span9" runat="server"></span></div>
                                        </div>
                                    </div>

                                    <div class="button-container">
                                        <asp:CheckBox ID="chkAdmin" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveAdmin" Width="100px" runat="server" Text="Save" OnClick="btnSaveAdmin_Click" class="button" />
                                        <asp:Button ID="btnUpdateAdmin" Width="100px" runat="server" Text="Update" OnClick="btnUSaveAdmin_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Admin (Cafeteria, Company Car, Guest House) End--%>

            <%--Laboratory Start--%>
            <div class="outer_wrap" runat="server" id="divLaboratory">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Laboratory</div>
                                    <div class="form_container">
                                        <div class="form_group">
                                            <label for="txtLabEquip">Laboratory Equipment Handed Over:</label>
                                            <%--<asp:TextBox ID="txtLabEquip" Width="300px" runat="server" class="form_input" ></asp:TextBox>--%>
                                            <asp:RadioButtonList ID="rblLabEquipHandedOver" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form_group">
                                            <label for="txtValueMiss">Value of Missing Equipment:</label>
                                            <asp:TextBox ID="txtValueMiss" Width="300px" runat="server" class="form_input"></asp:TextBox>
                                        </div>
                                        <div class="form_group">
                                            <label>Amount (If any):</label>
                                            <asp:TextBox ID="txtLabAmount" runat="server" Width="300px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtLabRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span4" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span5" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span6" runat="server"></span></div>
                                        </div>
                                    </div>

                                    <div class="button-container">
                                        <asp:CheckBox ID="chkLaboratory" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveLaboratory" Width="100px" runat="server" Text="Save" OnClick="btnSaveLaboratory_Click" class="button" />
                                        <asp:Button ID="btnUpdateLaboratory" Width="100px" runat="server" Text="Update" OnClick="btnUSaveLaboratory_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Laboratory End--%>

            <%--Training Start--%>
            <div class="outer_wrap" runat="server" id="divTraining">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Training</div>

                                    <div class="form_group">
                                        <label>Training Attended:</label>
                                        <!-- Grid View Container -->
                                        <div class="panel_body">
                                            <asp:GridView ID="gvTraining" runat="server" SkinID="GridView" CssClass="grid_view"
                                                AutoGenerateColumns="False" HorizontalAlign="Center" PageSize="1000" EmptyDataText="No Data Exist!">
                                                <Columns>
                                                    <asp:BoundField DataField="TRAINING_COURSES_DEDUCTION" SortExpression="TRAINING_COURSES_DEDUCTION" HeaderText="TRAINING_COURSES_DEDUCTION" />
                                                    <%--<asp:TemplateField HeaderText="Training Attended">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form_input" Text='<%# Bind("TrainingAttended") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deduction against Training">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDeduction" runat="server" CssClass="form_input" Text='<%# Bind("DeductionAgainstTraining") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form_input datepicker" Text='<%# Bind("Date") %>' DataFormatString="{0:dd/MM/yyyy}" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtTrainingRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="Span1" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span2" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="Span3" runat="server"></span></div>
                                        </div>
                                    </div>

                                    <!-- Save Button -->
                                    <div class="button-container">
                                        <asp:CheckBox ID="chkTraining" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveTraining" Width="100px" runat="server" Text="Save" OnClick="btnSaveTraining_Click" class="button" />
                                        <asp:Button ID="btnUpdateTraining" Width="100px" runat="server" Text="Update" OnClick="btnUSaveTraining_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <%--Training End--%>

            <%-- Head of Department Start--%>
            <div class="outer_wrap" runat="server" id="divHod">
                <div class="body_content">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <div class="panel">
                                    <div class="panel_header">Head of Department</div>
                                    <div class="form_container">
                                        <!-- HOD Form Fields -->
                                        <div class="form_group">
                                            <label for="txtNotice">Notice Period Served:</label>
                                            <%--<asp:TextBox ID="txtNotice" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblNoticeServed" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtFromDate">From Date*:</label>
                                            <asp:TextBox ID="txtFromDate" Width="300px" runat="server" class="form_input datepicker" DataFormatString="{0:dd/MM/yyyy}" />
                                        </div>
                                        <div class="form_group">
                                            <label for="txtToDate">To Date*:</label>
                                            <asp:TextBox ID="txtToDate" Width="300px" runat="server" class="form_input datepicker" DataFormatString="{0:dd/MM/yyyy}" />
                                        </div>

                                        <!-- Leave and Rehire Info -->
                                        <div class="form_group">
                                            <label for="txtRehireCallhod">Rehire Call:</label>
                                            <%--<asp:TextBox ID="txtRehireCallhod" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblRehireCallhod" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtRehireCallhodReason">In Case of No, Specify Reason:</label>
                                            <asp:TextBox ID="txtRehireCallhodReason" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <div class="form_group">
                                            <label for="txtLeavesDuringNotice">Leaves During Notice Period:</label>
                                            <asp:TextBox ID="txtLeavesDuringNotice" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <!-- Handing Over and Balances -->
                                        <div class="form_group">
                                            <label for="txtHandingOverhod">Handing Over Complete:</label>
                                            <%--<asp:TextBox ID="txtHandingOverhod" Width="300px" runat="server" class="form_input"  />--%>
                                            <asp:RadioButtonList ID="rblHandingOverhod" runat="server" CssClass="form_input" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form_group">
                                            <label for="txtClBlc">CL Balance:</label>
                                            <asp:TextBox ID="txtClBlc" Width="300px" runat="server" class="form_input" />
                                        </div>
                                        <div class="form_group">
                                            <label for="txtAlBalance">AL Balance:</label>
                                            <asp:TextBox ID="txtAlBalance" Width="300px" runat="server" class="form_input" />
                                        </div>
                                    </div>
                                    <div class="form_group">
                                        <label>Remarks:</label>
                                        <asp:TextBox ID="txtHodRemarks" runat="server" TextMode="MultiLine" Rows="5" Columns="5"></asp:TextBox>
                                    </div>
                                    <div class="container">
                                        <div class="row">
                                            <div class="column" style="color: yellow;"><strong>Verified by:</strong></div>
                                            <div class="column" style="color: #fff;">Name:</div>
                                            <div class="column" style="font-weight: bolder"><span id="spName" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Employee code:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="spEmpCode" runat="server"></span></div>
                                            <div class="column" style="color: #fff;"><strong>Designation:</strong></div>
                                            <div class="column" style="font-weight: bolder"><span id="spDesg" runat="server"></span></div>
                                        </div>
                                    </div>
                                    <!-- Save Button -->
                                    <div class="button-container">
                                        <asp:CheckBox ID="chkHOD" runat="server" Text="Approve" />
                                        <asp:Button ID="btnSaveHOD" Width="100px" runat="server" Text="Save" OnClick="btnSaveHOD_Click" class="button" />
                                        <asp:Button ID="btnUpdateHOD" Width="100px" runat="server" Text="Update" OnClick="btnUSaveHOD_Click" class="button" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>