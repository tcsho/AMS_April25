<%@ Page Title="Ramadan Timing" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="RamadanTiming.aspx.cs" Inherits="RamadanTiming" %>

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
                    bindMachineDataTable();
                }
                function bindMachineDataTable() {

                    jq('table.datatableRamadanTiming').DataTable({
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
                //Re-bind for callbacks
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    document_Ready();
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
                                    Ramadan Timing
                                </p>
                            </div>
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                                <br />
                            </div>
                        </div>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px" Enabled="false" AutoPostBack="True" Visible="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged" Visible="false">
                            </asp:DropDownList>
                            <asp:Button ID="btnAddNewPlan" runat="server" CssClass=" btn round_corner" Text="Add New"
                                Font-Names="Arial" Font-Size="11px" CausesValidation="false" Style="float: right;"
                                OnClick="btnAddNewPlan_Click" />
                        </div>
                        <asp:Panel runat="Server" ID="AddNew" class="panel" Visible="false">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <asp:GridView ID="gvCenter" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvCenters_RowDataBound"
                                            HorizontalAlign="Center" SkinID="GridView" BackColor="white" CssClass="table table-hover" OnRowCommand="gvCenters_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" OnPageIndexChanging="gvCenter_PageIndexChanging" OnSorting="gvCenter_Sorting"
                                            HeaderStyle-BackColor="#c6c6c6">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id">
                                                    <ItemStyle CssClass="hide" />
                                                    <HeaderStyle CssClass="hide" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id"></asp:BoundField>
                                                <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name"></asp:BoundField>
                                                <asp:TemplateField ShowHeader="False" HeaderText="Check">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="cbAllow" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="tr1" />
                                            <HeaderStyle CssClass="tableheader" />
                                            <AlternatingRowStyle CssClass="tr2" />
                                            <SelectedRowStyle CssClass="tr_select" />
                                        </asp:GridView>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="trNote" Visible="false" ForeColor="white" Text="Note: Changes will be made for all the centers in your region"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblhidden" ForeColor="white" Text="" Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lblCenter" ForeColor="white" Text="Center Name: " Visible="false"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblAttdate" for="Attendance Date: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Effective From Date:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtfromDate" runat="server" class="datepicker" placeholder="Attendance Date"
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
                                                    <asp:TextBox ID="txtTodate" runat="server" class="datepicker" placeholder="Attendance Date"
                                                        CausesValidation="true">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTodate"
                                                        Display="Dynamic" ErrorMessage="To Attendance Date Required" SetFocusOnError="True"
                                                        ForeColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblStart" runat="server" for="Start Time : " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtStart" runat="server" CssClass="jtimepicker" placeholder="Start Time"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtStart"
                                                        Display="Dynamic" ErrorMessage="End time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblEnd" runat="server" for="End Time: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtEnd" runat="server" CssClass="jtimepicker" AutoPostBack="true"
                                                        placeholder="End Time">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtEnd"
                                                        Display="Dynamic" ErrorMessage="End time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" for="Friday Start Time : " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Friday Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtFridaySTime" runat="server" CssClass="jtimepicker" placeholder="Friday Start Time"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFridaySTime"
                                                        Display="Dynamic" ErrorMessage="Friday time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" for="Friday End Time: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Friday End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtFridayETime" runat="server" CssClass="jtimepicker" AutoPostBack="true"
                                                        placeholder="Friday End Time">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtFridayETime"
                                                        Display="Dynamic" ErrorMessage="Friday time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblAbsent" runat="server" for="Absent Time: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Absent Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtAbsent" runat="server" CssClass="jtimepicker" placeholder="Absent Time ">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAbsent"
                                                        Display="Dynamic" ErrorMessage="Absent time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblTchStart" runat="server" for="Teacher Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher Start Time</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txttchStart" runat="server" CssClass="jtimepicker" placeholder="Teacher Start Time "></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txttchStart"
                                                        Display="Dynamic" ErrorMessage="Teacher time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblTchEnd" runat="server" for="Teacher End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtTchEnd" runat="server" CssClass="jtimepicker" placeholder="Teacher End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTchEnd"
                                                        Display="Dynamic" ErrorMessage="Teacher time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label4" runat="server" for="Teacher Friday Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher Friday Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtTeacherFSTime" runat="server" CssClass="jtimepicker" placeholder="Teacher Friday Start Time "></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTeacherFSTime"
                                                        Display="Dynamic" ErrorMessage="Teacher Friday time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" for="Teacher Friday End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher Friday End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtTeacherFETime" runat="server" CssClass="jtimepicker" placeholder="Teacher End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTeacherFETime"
                                                        Display="Dynamic" ErrorMessage="Teacher Friday Time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                             <div class="form-group" runat="server" visible="false">
                                                <asp:Label ID="Label12" runat="server" for="Teacher Absent Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher Absent Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtteachAbsentTime" runat="server" CssClass="jtimepicker" placeholder="Teacher End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtteachAbsentTime"
                                                        Display="Dynamic" ErrorMessage="Teacher Absent Time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" for="Network Office Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Network Office Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtNOSTime" runat="server" CssClass="jtimepicker" placeholder="Network Office Start Time "></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label7" runat="server" for="Network Office End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Network Office End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtNOETime" runat="server" CssClass="jtimepicker" placeholder="Network Office End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label8" runat="server" for="Network Office Friday Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Network Office Friday Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtNOFSTime" runat="server" CssClass="jtimepicker" placeholder="Network Office Friday Start Time "></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" for="Network Office Friday End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Network Office Friday End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtNOFETime" runat="server" CssClass="jtimepicker" placeholder="Network Office Friday End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" for="Saturday Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Saturday Start Time::</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtSaturdaSTime" runat="server" CssClass="jtimepicker" placeholder="Saturday Start Time: "
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSaturdaSTime"
                                                        Display="Dynamic" ErrorMessage="Teacher Friday time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server" for="Saturday End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Saturday End Time::</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtSaturdaETime" runat="server" CssClass="jtimepicker" placeholder="Saturday End Time:"
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSaturdaETime"
                                                        Display="Dynamic" ErrorMessage="Teacher Friday time Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblRemarks" runat="server" for="Remarks: " ForeColor="white" class="  col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Remarks: </asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="3" runat="server" placeholder="Remarks"
                                                        Width="227px" CausesValidation="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemarks"
                                                        Display="Dynamic" ErrorMessage="Remarks Required" SetFocusOnError="True" ForeColor="white"
                                                        ValidationGroup="c">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12">
                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" EnableClientScript="true"
                                                        HeaderText="Please ensure you have updated the following correctly:" runat="server"
                                                        ValidationGroup="c" ForeColor="white" />
                                                </div>
                                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                                                </div>
                                            </div>
                                            <div class="col-lg-12 text-right">
                                                <div class="pull-right">
                                                    <asp:Button ID="btnProceed" runat="server" Text="Save" ValidationGroup="c" class="btn btn-success"
                                                        OnClick="btnProceed_Click" CausesValidation="true" />
                                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%-------------------------------------------------------------------------Grid Panel----------------------------------------------------------------------                --%>
                        <asp:Panel runat="Server" ID="gridPanel">
                            <div class="panel">
                                <div class="panel_head">
                                    List of Campuses with Special Time
                                </div>
                                <div class="panel-body" style="overflow-x: auto;">
                                    <asp:GridView ID="gvDetails" runat="server" Width="100%" AutoGenerateColumns="False" OnPreRender="gvDetails_PreRender"
                                        HorizontalAlign="Center" SkinID="GridView"
                                        BackColor="white" CssClass="datatableRamadanTiming table table-hover " HeaderStyle-BackColor="#c6c6c6">
                                        <Columns>
                                            <asp:BoundField DataField="RT_Id" HeaderText="" SortExpression="RT_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StartDate" HeaderText="Attendance Date" SortExpression="AttDate"
                                                DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="EndDate" HeaderText="Attendance Date" SortExpression="AttDate"
                                                DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>

                                            <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name"></asp:BoundField>

                                            <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                            <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                                            <asp:BoundField DataField="Remarks" HeaderText=" Remarks  " SortExpression="Margin" />
                                            <asp:BoundField DataField="AbsentTime" HeaderText=" AbsentTime" SortExpression="AbsentTime" />
                                            <asp:BoundField DataField="FridayStartTime" HeaderText="Friday Start Time" SortExpression="FridayStartTime" />
                                            <asp:BoundField DataField="FridayEndTime" HeaderText="Friday End Time" SortExpression="FridayEndTime" />

                                            <asp:BoundField DataField="TeacherStartTime" HeaderText="Teacher Start Time" SortExpression="TeacherStartTime" />
                                            <asp:BoundField DataField="TeacherEndTime" HeaderText="Teacher End Time" SortExpression="TeacherEndTime" />
                                            <asp:BoundField DataField="TeacherFridayStartTime" HeaderText="Teacher Friday Start Time" SortExpression="TeacherFridayStartTime" />
                                            <asp:BoundField DataField="TeacherFridayEndTime" HeaderText="Teacher Friday End Time" SortExpression="TeacherFridayEndTime" />
                                            <asp:BoundField DataField="NOStart_Time" HeaderText="NO Start Time" SortExpression="NOStart_Time" />
                                            <asp:BoundField DataField="NOEnd_Time" HeaderText="NO End Time" SortExpression="NOEnd_Time" />
                                            <asp:BoundField DataField="NOFridaySTime" HeaderText="NO Friday Start Time" SortExpression="NOFridaySTime" />
                                            <asp:BoundField DataField="NOFridayETime" HeaderText="NO Friday End Time" SortExpression="NOFridayETime" />
                                            <asp:BoundField DataField="NOAbsentTime" HeaderText="NO Absent Time" SortExpression="NOAbsentTime" />
                                            <asp:BoundField DataField="SaturdayStartTime" HeaderText="Saturday Start Time" SortExpression="SaturdayStartTime" />
                                            <asp:BoundField DataField="SaturdayEndTime" HeaderText="Saturday End sTime" SortExpression="SaturdayEndTime" />
                                            <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("RT_Id") %>'
                                                        CausesValidation="False" CommandName="Delete Details" OnClick="btnDelete_Click"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
