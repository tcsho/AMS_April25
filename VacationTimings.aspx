<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="VacationTimings.aspx.cs" Inherits="VacationTimings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/jquery.timepicker.css" rel="Stylesheet" type="text/css" />
            <script src="Scripts/jquery.timepicker.js" type="text/javascript"></script>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Vacation Timings</p>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px" Enabled="true" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div style="clear: both;">
                            </div>
                            <asp:Button ID="btnAddNewVacation" runat="server" CssClass=" btn round_corner" Text="Add New Vacation"
                                CausesValidation="false" OnClick="btnAddNewVacation_Click" Style="float: right;" />
                            <asp:Button runat="server" ID="btnDeleteVacy" CssClass="btn btn-danger" CausesValidation="false"
                                OnClick="btnDeleteVacy_Click" Text="Delete Vacation " Style="float: right;" />
                            <%--<asp:Button ID="btnGenerateProcess" runat="server" CssClass=" btn round_corner"
                                CausesValidation="False" OnClick="btnGenerateProcess_Click" Text="Generate Shifts"
                                Width="106px" />--%>
                            <%--<asp:Button ID="btnSingleEmpProcess" runat="server" CssClass=" btn round_corner"
                                CausesValidation="False" OnClick="btnSingleEmpProcess_Click" Text="Attendance Refresh"
                                Width="130px" />--%>
                            <%--<asp:Button ID="btnMakeHOD_Employee" runat="server" CssClass=" btn round_corner" OnClientClick="return confirm('Are you sure to perform this action.')" 
                                CausesValidation="False" Text="." Width="130px" OnClick="btnMakeHOD_Employee_Click" Style="display: none" />--%>
                        </div>
                        <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <%--Change Timings--%>
                                    <p>
                                        FromDate:</p>
                                    <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" CssClass="form-control  datepicker"></asp:TextBox>
                                    <p>
                                        ToDate:</p>
                                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="30" CssClass="form-control datepicker"></asp:TextBox>
                                    <p>
                                        Reason:
                                    </p>
                                    <asp:TextBox ID="txt_Reason" runat="server" CssClass="form-control"></asp:TextBox>
                                    <p>
                                        Start Time*
                                        <asp:CheckBox ID="cb_StartTime" runat="server" Text="Start Time*:" Visible="false"
                                            CssClass="form-control datepicker" Checked="true" OnCheckedChanged="cb_StartTime_CheckedChanged"
                                            AutoPostBack="true" />
                                    </p>
                                    <%--<asp:CheckBox ID="cbStartTime" runat="server" />--%>
                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass=" form-control jtimepicker"
                                        Enabled="true"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Start Time"
                                    ControlToValidate="txtStartTime"></asp:RequiredFieldValidator>--%>
                                    <p>
                                        Absent Time*:</p>
                                    <asp:TextBox ID="txtAbsentTime" runat="server" CssClass="form-control jtimepicker"
                                        Enabled="true"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Absent Time"
                                    ControlToValidate="txtAbsentTime"></asp:RequiredFieldValidator>--%>
                                    <p>
                                        End Time*:
                                        <asp:CheckBox ID="cb_EndTime" runat="server" Text="End Time*:" Checked="true" Visible="false"
                                            AutoPostBack="true" OnCheckedChanged="cb_EndTime_CheckedChanged" /></p>
                                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control jtimepicker" Enabled="true"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter End Time"
                                    ControlToValidate="txtEndTime"></asp:RequiredFieldValidator>--%>
                                    <p>
                                        <asp:CheckBox ID="cbIsoffTeacher" runat="server" Text="Working Day For Teacher" Checked="true"
                                            AutoPostBack="true" />
                                    </p>
                                    <br />
                                    <asp:Button ID="btnSaveVacation" runat="server" Text="Save" OnClick="btnSaveVacation_Click"
                                        CausesValidation="true" CssClass="btn btn-success" />
                                    <asp:Button ID="btnCancelVacation" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancelVacation_Click" CssClass="btn btn-danger" />
                                    <div class="row">
                                        <asp:GridView ID="gvCenter" runat="server" Width="100%" AutoGenerateColumns="False"
                                            HorizontalAlign="Center" SkinID="GridView" BackColor="white" CssClass="table table-hover"
                                            OnRowCommand="gvCenters_RowCommand" HeaderStyle-BackColor="#c6c6c6">
                                            <Columns>
                                                <asp:BoundField DataField="Center_ID" HeaderText="Center Id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Center_Name" HeaderText="Center Name"></asp:BoundField>
                                                <asp:TemplateField ShowHeader="False" HeaderText="Check">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="cbAllow" CssClass="checkbox" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pan_Delete" runat="server" class="panel new_event_wraper">
                            <div class="new_event">
                                <%--Change Timings--%>
                                <p>
                                    *Kindly Note that the vacation timings will be deleted for the whole Region.
                                </p>
                                <p>
                                    FromDate:
                                    <asp:DropDownList runat="server" ID="ddlFrom" AutoPostBack="True" OnSelectedIndexChanged="ddlFrom_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    ToDate:
                                    <asp:DropDownList runat="server" ID="ddlToDate">
                                    </asp:DropDownList>
                                </p>
                                <p>
                                    <asp:Button ID="btnDeleteSave" runat="server" Text="Delete" CausesValidation="true"
                                        OnClick="btnDeleteSave_Click" />
                                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancelDelete_Click" />
                                </p>
                            </div>
                        </asp:Panel>
                        <div class="panel">
                            <div class="panel_head">
                                Branch Vacations
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvShifts" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    EmptyDataText="No record found." HorizontalAlign="Center" OnPageIndexChanging="gvShifts_PageIndexChanging"
                                    OnRowDataBound="gvShifts_RowDataBound" OnSorting="gvShifts_Sorting" OnRowCommand="gvShifts_RowCommand"
                                    AllowPaging="True" AllowSorting="True" PageSize="200">
                                    <Columns>
                                        <asp:BoundField DataField="VacationTimings_id" HeaderText="VacationTimings_id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="From_date" SortExpression="From_date" HeaderText="From date"
                                            DataFormatString="{0: MM/dd/yyyy}"></asp:BoundField>
                                        <asp:BoundField DataField="To_date" SortExpression="To_date" HeaderText="To date"
                                            DataFormatString="{0: MM/dd/yyyy}"></asp:BoundField>
                                        <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason"></asp:BoundField>
                                        <asp:BoundField DataField="Time_in" SortExpression="Time_in" HeaderText="Start Time">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Absent_time" SortExpression="Absent_time" HeaderText="Absent Time">
                                            <ItemStyle Width="40px"></ItemStyle>
                                            <HeaderStyle></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Time_out" SortExpression="Time_out" HeaderText="End Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IsOffTeacher" SortExpression="IsOffTeacher" HeaderText="Is Off for Teacher">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Working Day For Teacher">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("IsOffTeacher"))==false %>'
                                                    class="glyphicon glyphicon-remove"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("IsOffTeacher"))==true %>'
                                                    class="glyphicon glyphicon-ok"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Update Details">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnUpdateDetails" runat="server" CommandArgument='<%# Eval("VacationTimings_id") %>'
                                                    CausesValidation="False" CommandName="Edit Details" OnClick="btnEdit_Click" ImageUrl="~/images/approval.png"
                                                    Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("VacationTimings_id") %>'
                                                    CausesValidation="False" CommandName="Delete Details" OnClick="btnDelete_Click"
                                                    OnClientClick="return confirm('Are you sure to perform this action?')" ImageUrl="~/images/delete.gif"
                                                    Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" Visible="false">
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <!--end panel_footer-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                $(document).ready(document_Ready);

                function document_Ready() {
                    $('.jtimepicker').timepicker({ 'timeFormat': 'H:i:s' });
                    /*Date Picker*/
                    $(function () {
                        $(".datepicker").datepicker();
                        $("#anim").change(function () {
                            $(".datepicker").datepicker("option", "showAnim", $(this).val());
                        });
                    });

                    /*Date Picker*/



                }

                //Re-bind for callbacks
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    document_Ready();
                }); 
                
                


            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
