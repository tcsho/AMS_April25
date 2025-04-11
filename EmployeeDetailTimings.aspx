<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeDetailTimings.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="EmployeeDetailTimings" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            </script>

            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/jquery.timepicker.css" rel="Stylesheet" type="text/css" />
            <script src="Scripts/jquery.timepicker.js" type="text/javascript"></script>
            <link runat="server" id="link3" href="https://cdn.datatables.net/tabletools/2.2.4/css/dataTables.tableTools.min.css"
                rel="stylesheet" type="text/css" />
            <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
            <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
                rel="stylesheet" type="text/css" />
            <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
            <script type="text/javascript">
                var jq = $.noConflict();
            </script>
            <script type="text/javascript" src="https://cdn.datatables.net/1.10.10/js/jquery.dataTables.min.js"></script>
            <script type="text/javascript" src="https://cdn.datatables.net/1.10.10/js/dataTables.bootstrap.min.js"></script>
            <script type="text/javascript" src="https://cdn.datatables.net/tabletools/2.2.4/js/dataTables.tableTools.min.js"></script>
            <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
            <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
            <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
            <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/pdfmake.min.js"></script>
            <script type="text/javascript" src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.18/build/vfs_fonts.js"></script>
            <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.html5.min.js"></script>
            
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Manage Employee Timings
                        </p>

                        <div class="form-group">
                            <div class ="form-objects">
                                <div class="panel_head">
                                    Define Shifts
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                    <div class="div-Centers">
                                        <div class="col-xl-12">

                                            <div style="color:#FFF;font-weight: bold;text-shadow: 5px 5px 5px #000; font-size:18px;">
                                                Select Centers:
                                            </div>
                                            <asp:RadioButton ID="rbAllCenters" runat="server" AutoPostBack="True" Checked="true" GroupName="a"
                                                Text="All Centers" OnCheckedChanged="rbAllCenters_CheckedChanged" />
                                            <asp:RadioButton ID="rbSpecificCenters" runat="server" AutoPostBack="True" GroupName="a"
                                                Text="Specific Center" OnCheckedChanged="rbSpecificCenters_CheckedChanged" />
                                            <div class="row" style="margin-left:1px">
                                                <br />
                                                <asp:CheckBox ID="chkDesignations" runat="server" AutoPostBack="true" OnCheckedChanged="chkDesignations_CheckedChanged"
                                                    Text="Designations" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <asp:Panel ID="CentersPanel" runat="server" CssClass="panel new_event_wraper" ForeColor="">
                                    <div class="panel_head">
                                        Centers
                                <br />
                                    </div>
                                    <asp:GridView ID="gvSpecificCenters" runat="server" Width="100%" AutoGenerateColumns="False" Visible="false"
                                        CssClass="table table-hover" BorderStyle="None" HorizontalAlign="Center" HeaderStyle-BackColor="Gray" OnRowCommand="gvCenters_RowCommand"
                                        BackColor="White" AutoSizeColumnsMode="fixed" EmptyDataText="No Data Exist!" PageSize="100" AllowPaging="true" DataKeyNames="Center_ID" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Center_ID" SortExpression="Center_ID" HeaderText="Center Code"></asp:BoundField>
                                            <asp:BoundField DataField="Center_Name" SortExpression="Center_Name" HeaderText="Center Name"></asp:BoundField>
                                            <asp:TemplateField HeaderText="cb">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelect" runat="server" data-CenterID='<%# Eval("Center_ID") %>' AutoPostBack="true" OnCheckedChanged="cbSelect_CheckedChanged"/>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="selectAllCheckboxes" runat="server" CausesValidation="False" CommandName="toggleCheck" Style="color: black; text-decoration: none">Select</asp:LinkButton>
                                                </HeaderTemplate>
                                                <HeaderStyle />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="DesignationsPanel" runat="server" CssClass="panel new_event_wraper" ForeColor="">
                                    <div class="panel_head">
                                        Designations
                                <br />
                                    </div>
                                    <asp:GridView ID="gvDesignations" runat="server" Width="100%" AutoGenerateColumns="False" Visible="true"
                                        CssClass="table table-hover" BorderStyle="None" HorizontalAlign="Center" HeaderStyle-BackColor="Gray" OnRowCommand="gvDesignations_RowCommand"
                                        BackColor="White" AutoSizeColumnsMode="fixed" EmptyDataText="No Data Exist!" PageSize="100" AllowPaging="true" DataKeyNames="DesigCode"  >
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemStyle Width="20px" />
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesigName" SortExpression="DesigName" HeaderText="Designation Name"></asp:BoundField>
                                            <asp:BoundField DataField="DesignationCount" SortExpression="DesignationCount" HeaderText="No. of Employees" />
                                            <asp:TemplateField HeaderText="cb">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbSelectDesig" runat="server" data-CenterID='<%# Eval("DesigCode") %>'/>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="SelectAllDesig" runat="server" CausesValidation="False" CommandName="toggleCheck" Style="color: black; text-decoration: none">Select</asp:LinkButton>
                                                </HeaderTemplate>
                                                <HeaderStyle />
                                                <ItemStyle />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel runat="Server" ID="AddNew" class="panel" Visible="false">
                                    <div class="panel-body">
                                        <div class="form-horizontal">
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblAttdate" for="Attendance Date: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Effective From Date:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtfromDate" runat="server" class="datepicker" placeholder="Attendance From Date"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfromDate"
                                                            Display="Dynamic" ErrorMessage="From Attendance Date Required" SetFocusOnError="True"
                                                            ForeColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="Label1" for="Attendance Date: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Effective To Date:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtTodate" runat="server" class="datepicker" placeholder="Attendance To Date"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTodate"
                                                            Display="Dynamic" ErrorMessage="To Attendance Date Required" SetFocusOnError="True"
                                                            ForeColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblReason" runat="server" for="Reason: " ForeColor="white" class="  col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Reason: </asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="3" runat="server" placeholder="Reason"
                                                            Width="227px" CausesValidation="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReason"
                                                            Display="Dynamic" ErrorMessage="Reason Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblTimeIn" for="Time In: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Time In:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtTimeIn" runat="server" CssClass="jtimepicker" placeholder="Time In"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTimeIn"
                                                            Display="Dynamic" ErrorMessage="Time In Required" SetFocusOnError="True"
                                                            ForeColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblTimeOut" for="Time Out: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Time Out:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtTimeOut" runat="server" CssClass="jtimepicker" placeholder="Time Out"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTimeOut"
                                                            Display="Dynamic" ErrorMessage="Time Out Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblAbsentTime" for="Absent Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Absent Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtAbsentTime" runat="server" placeholder="Absent Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAbsentTime"
                                                            Display="Dynamic" ErrorMessage="Absent Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblFriStartTime" for="Friday Start Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Friday Start Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtFriStartTime" runat="server" placeholder="Friday Start Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFriStartTime"
                                                            Display="Dynamic" ErrorMessage="Friday Start Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblFriAbsentTime" for="Friday Absent Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Friday Absent Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtFriAbsentTime" runat="server" placeholder="Friday Absent Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFriAbsentTime"
                                                            Display="Dynamic" ErrorMessage="Friday Absent Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblFriEndTime" for="Friday End Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Friday End Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtFriEndTime" runat="server" placeholder="Friday End Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFriEndTime"
                                                            Display="Dynamic" ErrorMessage="Friday End Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSatStartTime" for="Saturday Start Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Saturday Start Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtSatStartTime" runat="server" placeholder="Saturday Start Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSatStartTime"
                                                            Display="Dynamic" ErrorMessage="Saturday Start Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSatAbsentTime" for="Saturday Absent Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Saturday Absent Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtSatAbsentTime" runat="server" placeholder="Saturday Absent Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSatAbsentTime"
                                                            Display="Dynamic" ErrorMessage="Saturday Absent Time Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblSatEndTime" for="Saturday End Time: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Saturday End Time:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtSatEndTime" runat="server" placeholder="Saturday End Time" CssClass="jtimepicker"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtSatEndTime"
                                                            Display="Dynamic" ErrorMessage="Saturday End Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblMargin" for="Margin: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Margin:</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:TextBox ID="txtMargin" runat="server" placeholder="Margin"
                                                            CausesValidation="true">
                                                        </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtMargin"
                                                            Display="Dynamic" ErrorMessage="Margin Required" SetFocusOnError="True" ForeColor="white"
                                                            ValidationGroup="c">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label runat="server" ID="lblLocked" for="Locked: " ForeColor="white"
                                                        class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Lock</asp:Label>
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                        <asp:Checkbox ID="chkLock" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-lg-12 text-right">
                                                    <div class="pull-right">
                                                        <asp:Button ID="btnProceed" runat="server" Text="Apply Timings" ValidationGroup="c" class="btn btn-success"
                                                            OnClick="btnProceed_Click" CausesValidation="true" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="Server" ID="gridPanel">
                                <div class="panel">
                                    <div class="panel_head">
                                        Employee Shift Timings
                                    </div>
                                    <div class="panel-body" style="overflow-x: auto;">
                                        <asp:GridView ID="gvDetailTimings" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Data Exist!"
                                            HorizontalAlign="Center" SkinID="GridView"
                                            BackColor="white" CssClass="datatableDetailTimings table table-hover " HeaderStyle-BackColor="#c6c6c6"
                                            >
                                            <Columns>
                                                <asp:BoundField DataField="ShiftCaseId" HeaderText="" SortExpression="ShiftCaseId">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="FromDate" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                                                <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="ToDate" />
                                                <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="Reason" />
                                            
                                                <asp:TemplateField ShowHeader="False" HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnDateDetails" runat="server" Text="Date Details" class="btn btn-primary" CommandArgument='<%# Bind("ShiftCaseId") %>'
                                                          OnClick="btnDateDetails_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False" HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEmployeesWithTimings" runat="server" Text="Employees" class="btn btn-primary"  CommandArgument='<%# Bind("ShiftCaseId") %>'
                                                            OnClick="btnEmployeeDetails_Click"
                                                           />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:TemplateField>
                                            
                                            </Columns>
                                            <RowStyle CssClass="tr1" />
                                            <HeaderStyle CssClass="tableheader" />
                                            <AlternatingRowStyle CssClass="tr2" />
                                            <SelectedRowStyle CssClass="tr_select" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                                <asp:Panel runat="Server" ID="DateDetailPanel" Visible="false">
                            <div class="panel">
                                <div class="panel_head">
                                    Date Detail Timings
                                </div>
                                <div class="panel-body" style="overflow-x: auto;">
                                    <asp:GridView ID="gvDateDetail" runat="server" Width="100%" AutoGenerateColumns="False" 
                                        OnRowUpdating="gvDateDetail_RowUpdating"
                                        OnRowCancelingEdit="gvDateDetail_RowCancelingEdit"  
                                        OnRowEditing="gvDateDetail_RowEditing"
                                        EmptyDataText="No Data Exist!"
                                        DataKeyNames="ShiftCaseDateId"
                                        HorizontalAlign="Center" SkinID="GridView"
                                        BackColor="white" CssClass="datatableDetailTimings table table-hover " HeaderStyle-BackColor="#c6c6c6">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ShiftCaseDateId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblShiftCaseDateId" runat="server" Text='<%#Eval("ShiftCaseDateId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ShiftCaseId" HeaderText="" SortExpression="ShiftCaseId">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attendance Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttDate" runat="server" Text='<%#Eval("AttDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time In">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateDetailTimeIn" runat="server" Text='<%#Eval("TimeIn") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDateDetailTimeIn" runat="server" Text='<%#Eval("TimeIn") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Time Out">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateDetailTimeOut" runat="server" Text='<%#Eval("TimeOut") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDateDetailTimeOut" runat="server" Text='<%#Eval("TimeOut") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Absent Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateDetailAbsentTime" runat="server" Text='<%#Eval("AbsentTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDateDetailAbsentTime" runat="server" Text='<%#Eval("AbsentTime") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
<%--                                            <asp:TemplateField HeaderText="Is Off">
                                                <ItemTemplate>
                                                    <asp:Checkbox ID="chkDateDetailIsOff" runat="server" Text='<%#Eval("isOff") %>'></asp:Checkbox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDateDetailTimeOut" runat="server" Text='<%#Eval("Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btn_Edit" runat="server" ImageUrl="~/images/edit.gif" Text="Edit" CommandName="Edit" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btn_Update" runat="server" Text="Update" CommandName="Update" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel" CssClass="btn btn-primary"/>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                                <asp:Panel runat="Server" ID="ListOfEmployeesPanel">
                                <div class="panel">
                                    <div class="panel_head">
                                        Employees
                                    </div>
                                    <div class="panel-body" style="overflow-x: auto;">
                                        <asp:GridView ID="gvEmployeesAppliedTimings" runat="server" Width="100%" AutoGenerateColumns="False" EmptyDataText="No Data Exist!"
                                            HorizontalAlign="Center" SkinID="GridView"
                                            BackColor="white" CssClass="datatableEmployeesWithTimings table table-hover " HeaderStyle-BackColor="#c6c6c6" OnPreRender="gvEmployeesAppliedTimings_PreRender"
                                            Visible="false">
                                            <Columns>
                                                <asp:BoundField DataField="ShiftCaseEmpId" HeaderText="">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" HeaderText="EMP #" SortExpression="Employee Code"></asp:BoundField>
                                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="Center_Id" HeaderText="" SortExpression="Center_Id">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CenName" HeaderText="Center" SortExpression="CenName" />
                                                <asp:BoundField DataField="DesigCode" HeaderText="" SortExpression="DesigCode">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                                <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ShiftCaseEmpId") %>'
                                                            CausesValidation="False" CommandName="Delete Employees" OnClick="btnDelete_Click"
                                                            OnClientClick="return confirm('Are you sure to perform this action?')" ImageUrl="~/images/delete.gif"
                                                            Text="" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                            </Columns>
                                            <RowStyle CssClass="tr1" />
                                            <HeaderStyle CssClass="tableheader" />
                                            <AlternatingRowStyle CssClass="tr2" />
                                            <SelectedRowStyle CssClass="tr_select" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <script type="text/javascript">
                $(document).ready(document_Ready);
                
                function document_Ready() {

                    $('.jtimepicker').timepicker({ 'timeFormat': 'H:i:s' }); /*Date Picker*/
                    $(function () {
                        $(".datepicker").datepicker({
                            dateFormat: 'dd/mm/yy'
                        });
                        $("#anim").change(function () {
                            $(".datepicker").datepicker("option", "showAnim", $(this).val());
                        });
                    });
                    bindMachineDataTable()
                }
                function bindMachineDataTable() {

                    jq('table.datatableEmployeesWithTimings').DataTable({
                        "dom": 'lfBrtip',
                        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
                        "iDisplayLength": 25,
                        "buttons": [
                            { extend: 'excel', text: ' Excel', className: 'btn btn-info glyphicon glyphicon-export', filename: 'Facial_Machine_Details', exportOptions: { modifier: { page: 'all' } } },
                            { extend: 'pdf', text: ' PDF', className: 'btn btn-info glyphicon glyphicon-export', filename: 'Facial_Machine_Details', orientation: 'landscape', pageSize: 'LEGAL', exportOptions: { modifier: { page: 'all' }, columns: ':visible' } }
                        ],
                        "order": [[1, "asc"]],
                        "columnDefs": [{ visible: false, searchable: false, targets: [0] },
                        { bSortable: false, targets: [] }], //disable sorting on Add/Edit Buttons
                        "paging": true, "ordering": true, "searching": true, "info": true, "scrollX": false
                    });
                    $('.dataTables_filter').addClass('pull-right');
                    $('.dataTables_paginate').addClass('pull-right');
                    $('.dataTables_filter input').attr('title', 'Search is applicable on all columns of the table below');
                };
                //Re-bind for callbacks
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    document_Ready();
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
