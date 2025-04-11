<%@ Page Title="Facial Machine Status" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="FacialMachineStatus.aspx.cs" Inherits="FacialMachineStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link runat="server" id="link3" href="https://cdn.datatables.net/tabletools/2.2.4/css/dataTables.tableTools.min.css"
                rel="stylesheet" type="text/css" />
            <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.2/css/buttons.dataTables.min.css" />
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
            <style type="text/css">
                .hover_img a {
                    position: relative;
                }

                    .hover_img a span {
                        position: absolute;
                        display: none;
                        z-index: 99;
                    }

                    .hover_img a:hover span {
                        display: block;
                    }

                .centeralign {
                    float: right !important;
                    right: 300px !important;
                }
            </style>
            <script type="text/javascript">
                $(document).ready(function () {
                    bindMachineDataTable();
                    bindEmployeeDataTable();
                    $('.dataTables_filter').addClass('pull-right');
                    $('.dataTables_paginate').addClass('pull-right');
                    $('.dt-buttons').addClass('centeralign');

                });
                function bindMachineDataTable() {

                    jq('table.datatableMachine').DataTable({
                        "dom": 'lfBrtip',
                        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
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
                function bindEmployeeDataTable() {

                    jq('table.datatableEmployee').DataTable({
                        "dom": 'lfBrtip',
                        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
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
                $(document).ready(function () {
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindMachineDataTable);
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindEmployeeDataTable);
                });
            </script>

            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="form-group">
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                            </div>
                            <div class=" col-lg-6 col-md-6 col-sm-6 col-xs-12 display-2">
                                <p class="page_heading">
                                    Facial Device Machine Status
                                </p>
                            </div>
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                                <br />
                            </div>
                        </div>
                        <div class="controls round_corner" runat="server" id="divddl">
                            <asp:Button runat="server" ID="btnViewReport" Text="View Report" OnClick="btnViewReport_Click" Visible="false" />
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="false" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDStatus" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="btnActive_Click">
                                <asp:ListItem Value="1" Text="Instock Deployed" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Instock but not deployed"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="controls round_corner" runat="server" id="divButtons">
                            <div class="col-lg-12 pull-right">
                                <asp:Button runat="server" ID="btnAll" CssClass="btn btn-primary" Text="All Devices"
                                    OnClick="btnAll_Click" />
                                <asp:Button runat="server" ID="btnICU" CssClass="btn btn-primary" Text="ICU" OnClick="btnICU_Click" />
                                <asp:Button runat="server" ID="btnDead" CssClass="btn btn-primary" Text="Dead Devices"
                                    OnClick="btnDead_Click" />
                                <asp:Button runat="server" ID="btnAlive" CssClass="btn btn-primary" Text="Alive Devices"
                                    OnClick="btnAlive_Click" />
                                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="Search Employee"
                                    OnClick="btnSearch_Click" />
                                <div class=" pull-right">
                                    <asp:Button runat="server" ID="btnNew" CssClass="btn btn-primary" Text="Add a Facial Device"
                                        OnClick="btnAdd_Click" />
                                    <asp:Button runat="server" ID="btnCheckAll" Text="Check All Connectivity" CssClass="btn btn-success" OnClick="btnCheckAll_Click" />
                                    <p>
                                </div>
                            </div>
                        </div>

                        <asp:Panel runat="Server" ID="pan_Search" class="panel" Visible="false">
                            <div class="panel_head">
                                Search Employee
                            </div>
                            <div class="panel-body">
                                <asp:Label runat="server" ID="lblsearch" Text="Employee Code: " CssClass="col-lg-4 col-md-4 col-sm-4 col-xs-4 TextLabelMandatory"
                                    ForeColor="White"></asp:Label>
                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                    <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control textbox"
                                        OnTextChanged="txtEmployeeCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div class=" pull-right">
                                        <asp:Button runat="server" ID="btncancelS" CssClass="btn btn-primary" Text="Cancel"
                                            OnClick="btnCancelSearch_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="gridPanel" class="panel">
                            <div class="panel_head">
                                Details of Facial Machines
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="gvCenter" runat="server" AutoGenerateColumns="False" OnPreRender="gvCenter_PreRender"
                                    BorderStyle="None" HeaderStyle-BackColor="#c6c6c6" BackColor="white"
                                    CssClass="datatableMachine table table-striped table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="1%" />
                                            <HeaderStyle Width="1%" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MaxDeviceId" HeaderText="MaxDeviceId" SortExpression="MaxDeviceId">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeviceID" HeaderText="Device ID" SortExpression="DeviceID">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" SortExpression="BranchCode" />
                                        <asp:BoundField DataField="DeviceName" HeaderText="Device Name" SortExpression="DeviceName" />
                                        <asp:BoundField DataField="DeviceIP" HeaderText="Device IP" SortExpression="DeviceIP" />
                                        <asp:BoundField DataField="DevicePort" HeaderText="Device Port" SortExpression="DevicePort"></asp:BoundField>
                                        <asp:BoundField DataField="DeviceSerialNo" HeaderText="Device Serial Number" SortExpression="DeviceSerialNo"></asp:BoundField>
                                        <asp:BoundField DataField="RegionID" HeaderText="RegionID" SortExpression="RegionID">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PushLastConnection" HeaderText="HeartBeat Date" SortExpression="PushLastConnection" DataFormatString="{0:dd-MMM-yyyy hh:mm:ss}"></asp:BoundField>
                                        <asp:BoundField DataField="PullLastConnection" HeaderText="Last Connected At" SortExpression="PullLastConnection" DataFormatString="{0:dd-MMM-yyyy  hh:mm:ss}"></asp:BoundField>
                                        <asp:BoundField DataField="DeviceStatus" HeaderText=" Device Status" SortExpression="DeviceStatus" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Update Details">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnUpdateDetails" runat="server" CommandArgument='<%# Eval("DeviceID") %>'
                                                    CausesValidation="False" Visible="true" CommandName="Edit Device Details" CssClass="glyphicon glyphicon-pencil"
                                                    OnClick="btnUpdate_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Check Connectivity">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnConnect" Class="glyphicon glyphicon-refresh" CommandArgument='Connect'
                                                    OnClick="btnConnect_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Device Info">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="LinkButton2" Class="glyphicon glyphicon-info-sign" CommandArgument='Info'
                                                    OnClick="btnInfo_Click"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <RowStyle CssClass="tr1" Wrap="true" HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="tableheader" Wrap="true" />
                                    <AlternatingRowStyle CssClass="tr2" />
                                    <SelectedRowStyle CssClass="tr_select" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="Server" ID="pan_AddNew" Visible="false">
                            <div class="panel">
                                <div class="panel_head">
                                    Add a New Device
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 form-group">
                                        <asp:Label runat="server" Text="*Device ID" ForeColor="White" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:Label CssClass="TextLabelMandatory40" runat="server" ForeColor="White" Text=""
                                                ID="lblDeviceId"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblBC" runat="server" Text="*Branch Code" ForeColor="White" CssClass=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:TextBox class="form-control textbox" runat="server" Text="" ID="txtBranchCode">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ValidationGroup="add"
                                                CssClass="errormesg" ControlToValidate="txtBranchCode" ErrorMessage="Branch Code Required!"
                                                ForeColor="White" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblDN" runat="server" Text="*Device Name" ForeColor="White" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:TextBox class="form-control textbox" runat="server" Text="" ID="txtDeviceName">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ValidationGroup="add"
                                                CssClass="errormesg" ControlToValidate="txtDeviceName" ErrorMessage="Device Name Required!"
                                                ForeColor="White" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblSN" runat="server" Text="*Serial Number" ForeColor="White" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:TextBox class="form-control textbox" runat="server" Text="" ID="txtSerialNum">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ValidationGroup="add"
                                                CssClass="errormesg" ControlToValidate="txtSerialNum" ErrorMessage="Serial Number Required!"
                                                ForeColor="White" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblDS" runat="server" Text="*Device Status" ForeColor="White" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:DropDownList runat="server" class="form-control dropdown" AutoPostBack="true"
                                                ID="ddlDeviceStatus">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="Active" Text="Active" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="Deactive" Text="Deactive"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlDeviceStatus" ID="RequiredFieldValidator1"
                                                ForeColor="White" ValidationGroup="add" CssClass="errormesg" ErrorMessage="Status Required"
                                                InitialValue="0" runat="server" Display="Dynamic">
                                            </asp:RequiredFieldValidator>

                                            <br />
                                            </p>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class=" col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                            <div class="hover_img">
                                                <asp:Label ID="lblIP" runat="server" Text="*Device IP: " ForeColor="White" CssClass="control-label">
                                                </asp:Label>
                                                <a href="#" class="glyphicon glyphicon-info-sign"><span>
                                                    <img src="images/IP.jpg" alt="image" height="100" />
                                                </span></a>
                                            </div>
                                        </div>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:TextBox class="form-control textbox" runat="server" Text="" ID="txtIP">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ValidationGroup="add"
                                                ForeColor="White" CssClass="errormesg" ControlToValidate="txtIP" ErrorMessage="IP Required!" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPort" runat="server" Text="*Device Port" ForeColor="White" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">
                                        </asp:Label>
                                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                            <asp:DropDownList runat="server" class="form-control dropdown" AutoPostBack="true"
                                                ID="ddlPort">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlPort" ID="RequiredFieldValidator2"
                                                ForeColor="White" ValidationGroup="add" CssClass="errormesg" ErrorMessage="Port Required"
                                                InitialValue="0" runat="server" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-lg-12 text-right">
                                            <asp:Button runat="server" ID="btnSaveDevice" CausesValidation="true" CssClass="btn btn-primary"
                                                ValidationGroup="add" Text="Save Device" OnClick="btnSaveDevice_Click" />
                                            <asp:Button runat="server" ID="btnCancel" CausesValidation="false" CssClass="btn btn-danger"
                                                ValidationGroup="add" Text="Cancel" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pan_operations" CssClass="panel" Visible="false" Style="overflow: auto;">
                            <div class="panel_head">
                                Device Operations
                            </div>
                            <div class="panel-body">
                                <asp:Label runat="server" ID="lblAdmin" CssClass="TextLabelMandatory" ForeColor="White"></asp:Label>
                                <asp:Label runat="server" ID="lblPull" CssClass="TextLabelMandatory40" ForeColor="White"></asp:Label>
                                <asp:Label runat="server" ID="lblTime" CssClass="TextLabelMandatory40" ForeColor="White"></asp:Label>
                                <div>
                                    <asp:GridView ID="gvDeviceDetails" runat="server" AutoGenerateColumns="False"
                                        BorderStyle="None" HorizontalAlign="Center" HeaderStyle-BackColor="#c6c6c6" BackColor="white"
                                        CssClass="table table-condensed gridmargin" AutoSizeColumnsMode="fixed">
                                        <Columns>
                                            <asp:BoundField DataField="DeviceIP" HeaderText="Device IP" SortExpression="DeviceIP">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DevicePort" HeaderText="Device Port" SortExpression="DevicePort">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DeviceID" HeaderText="Device ID" SortExpression="DeviceID">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="ConnectivityStatus">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ConnectivityDate" HeaderText="Last Checked On" SortExpression="ConnectivityDate" DataFormatString="{0:dd-MMM-yyyy}">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SerialNumber" HeaderText="Serial #" SortExpression="SerialNumber">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Firmware" HeaderText="Firmware" SortExpression="Firmware">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Gateway" HeaderText="Gateway" SortExpression="Gateway">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MacAddress" HeaderText="Mac Address" SortExpression="MacAddress">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NetMask" HeaderText="Net Mask" SortExpression="NetMask">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Algorithm" HeaderText="Algorithm" SortExpression="Algorithm"></asp:BoundField>
                                            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StoragePercentage" HeaderText="Storage Percentage" SortExpression="StoragePercentage"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Last Comments">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtComments" Width="150px"
                                                        OnTextChanged="txtComments_TextChanged" Text='<%# Eval("Comments")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" Wrap="true" HorizontalAlign="left" />
                                        <HeaderStyle CssClass="tableheader" Wrap="false" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                                </div>

                                <asp:GridView ID="gvOperations" runat="server" AutoGenerateColumns="False" OnPreRender="gvOperations_PreRender"
                                    BorderStyle="None" HorizontalAlign="Center" HeaderStyle-BackColor="#c6c6c6" BackColor="white"
                                    CssClass="table table-condensed" AutoSizeColumnsMode="fixed">
                                    <Columns>
                                        <asp:BoundField DataField="DeviceIP" HeaderText="Device IP" SortExpression="DeviceIP">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DevicePort" HeaderText="Device Port" SortExpression="DevicePort">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeviceID" HeaderText="Device ID" SortExpression="DeviceID">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>


                                        <asp:TemplateField ShowHeader="False" HeaderText="Sync Machine Time">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnGetTime" runat="server" CommandArgument='GetDateTime()' CausesValidation="False"
                                                    CssClass="glyphicon glyphicon-time" OnClick="btnTimeClick" />
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='SetDateTime()' CausesValidation="False"
                                                    CssClass="glyphicon glyphicon-refresh" OnClick="btnSyncTimeClick" />
                                                <asp:Label runat="server" ID="lblTimeSync" CssClass="TextLabelMandatory40"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Pull Users">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnUsers" runat="server" CausesValidation="False" OnClick="btnPullUser_Click"
                                                    CssClass="glyphicon glyphicon-user" CommandArgument="GetEmployeeID()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Uploading Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnUploading" runat="server" CommandArgument='GetDateTime()'
                                                    CausesValidation="False" CssClass="glyphicon glyphicon-open" OnClick="btnStatusClick" />
                                                <asp:Label runat="server" ID="lblStatus" CssClass="TextLabelMandatory40"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="All Records" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPull" runat="server" CausesValidation="False" CssClass="glyphicon glyphicon-open"
                                                    OnClick="btnPull_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Pull Records" Visible="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPullRange" runat="server" CausesValidation="False" CssClass="glyphicon glyphicon-download-alt"
                                                    OnClick="btnPullRange_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="False" HeaderText="Pull Admins">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdmin" runat="server" CausesValidation="False" OnClick="btnGetManagerClick"
                                                    CssClass="glyphicon glyphicon-lock" CommandArgument="GetManagerId()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="tr1" Wrap="true" HorizontalAlign="left" />
                                    <HeaderStyle CssClass="tableheader" Wrap="false" />
                                    <AlternatingRowStyle CssClass="tr2" />
                                    <SelectedRowStyle CssClass="tr_select" />
                                </asp:GridView>
                                <div class="pull-right">
                                    <asp:Button ID="btnCancelOp" runat="server" CssClass="btn btn-danger" CausesValidation="False"
                                        Text="Cancel" OnClick="btnCancelOPClick" />
                                </div>
                                <p>
                                    <br />
                                    <br />
                                </p>
                                <div runat="server" class="form-group" id="divPull" visible="false">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12  form-group">
                                        <asp:Label ID="Label2" CssClass="TextLabelMandatory" runat="server" Text="Please Specify a Date Range"
                                            ForeColor="White"></asp:Label>
                                        <asp:Label ID="lblPullIP" CssClass=" TextLabelMandatory" runat="server" ForeColor="White"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblPullPort" CssClass=" TextLabelMandatory" runat="server" ForeColor="White"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblPullID" CssClass=" TextLabelMandatory" runat="server" ForeColor="White"
                                            Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label CssClass="col-lg-4 col-md-4 col-sm-4 col-xs-4 TextLabelMandatory" runat="server"
                                            Text="Date From: " ForeColor="White">
                                        </asp:Label>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <asp:TextBox runat="server" ID="txtFrom" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" CssClass="col-lg-4 col-md-4 col-sm-4 col-xs-4 TextLabelMandatory"
                                            runat="server" Text="Date To: " ForeColor="White">
                                        </asp:Label>
                                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <asp:TextBox runat="server" ID="txtTO" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-danger" Text="Proceed" OnClick="btnProceed_Click" />
                                        <asp:Button ID="btnCancelPul" runat="server" CssClass="btn btn-danger" Text="Cancel"
                                            OnClick="btnCancelPull_Click" />
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pan_Employee" class="panel" Visible="false">
                            <div class="panel_head">
                                List of Employees
                            </div>
                            <div class="panel-body">
                                <asp:LinkButton runat="server" Text="Get Device Users" ID="btnUser" Visible="false"
                                    CommandArgument="GetEmployeeID()" CssClass="btn btn-danger" />
                                <asp:LinkButton runat="server" Text="Get Device Admin" ID="btnGetManager" Visible="false"
                                    CssClass="btn btn-danger" />
                                <asp:Label runat="server" ID="lblGridStatus" ForeColor="White" CssClass="TextLabelMandatroy"></asp:Label>
                                <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    HorizontalAlign="Center" CssClass="datatableEmployee table table-condensed" HeaderStyle-BackColor="#c6c6c6"
                                    OnPreRender="gvEmployees_PreRender">
                                    <Columns>

                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeTerminalID" HeaderText="Employee Number" SortExpression="EmployeeTerminalID"></asp:BoundField>
                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FullName"></asp:BoundField>
                                        <asp:BoundField DataField="RegionName" HeaderText="Region Name" SortExpression="RegionName"></asp:BoundField>
                                        <asp:BoundField DataField="CenterName" HeaderText="Center Name" SortExpression="CenterName"></asp:BoundField>
                                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department"></asp:BoundField>
                                        <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation"></asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Employement Status" SortExpression="Status"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnDelete" Class="glyphicon glyphicon-remove"
                                                    CommandArgument='<%# Eval("EmployeeTerminalID") %>' OnClientClick="return confirm('Are you sure to perform this action?')"
                                                    OnClick="btnDeleteClick"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Set Manager" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnsetManager" Class="glyphicon glyphicon-lock" Visible="false"
                                                    CommandArgument='<%# Eval("EmployeeCode") %>' OnClick="setManager" OnClientClick="return confirm('Are you sure to perform this action?')"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="tr1" />
                                    <HeaderStyle CssClass="tableheader" />
                                    <AlternatingRowStyle CssClass="tr2" />
                                    <SelectedRowStyle CssClass="tr_select" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <asp:Panel ID="Panel70" runat="server" CssClass="Pbar">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" />
                </div>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
