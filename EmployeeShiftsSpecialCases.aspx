<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeShiftsSpecialCases.aspx.cs" Inherits="EmployeeShiftsSpecialCases" %>

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
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px" AutoPostBack="True"
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
                            <asp:Button ID="btnAddNewSpecialCase" runat="server" CssClass=" btn round_corner"
                                Text="Add New Special Case" Font-Names="Arial" Font-Size="11px" CausesValidation="false"
                                OnClick="btnAddNewSpecialCase_Click" Style="float: right;" />
                        </div>
                        <asp:Panel ID="pan_New" runat="server" CssClass="panel new_event_wraper" ForeColor="">
                            <div class="panel_head">
                                Employee List
                                <br />
                            </div>
                            <asp:GridView ID="gvEmployees" runat="server" Width="100%" AutoGenerateColumns="False"  
                               CssClass="table table-hover"  BorderStyle="None" HorizontalAlign="Center"   HeaderStyle-BackColor="Gray" 
                               BackColor="White" AutoSizeColumnsMode="fixed" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemStyle Width="20px" />
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee Code">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Full Name">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" SortExpression="DeptName" HeaderText="Department Name">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DesigName" SortExpression="DesigName" HeaderText="Designation Name">
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbAllow" runat="server" data-EmployeeID ='<%# Eval("EmployeeCode") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle />
                                        <ItemStyle />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                           <asp:HiddenField ID="HiddenField1" runat="server" />
                            <div class="Reoprts_list" id="ShorForm" runat="server">
                                <div class="Report_criteria">
                                    <div class="Report_criteria_header">
                                        <asp:DropDownList ID="ddlSpecialCase_Type" runat="server" Width="323px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSpecialCase_Type_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div runat="server" class="Report_criteria_header" id="divCriteria">
                                            <asp:RadioButtonList ID="rbLstOpt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbLstRpt_SelectedIndexChanged"
                                                Width="100%">
                                                <asp:ListItem Value="0" Selected="True">Date Range</asp:ListItem>
                                                <asp:ListItem Value="1">Week Days</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="">
                                        <div class="checkbox pull-right" id="div_WeekDays" runat="server">
                                            <asp:CheckBoxList ID="cblWeekdays" runat="server" Width="100%" OnSelectedIndexChanged="cblWeekdays_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                                <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                                <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                                <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                                <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                                <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                                <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 form-group" runat="server" id="divData">
                                        <%--Change Timings--%>
                                        <div class=" form-group">
                                            <asp:Label runat="server" CssClass="control-label" Visible="false">
                                                    *Employee Name:</asp:Label>
                                            <div class="control-label">
                                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="dropdown" Visible="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="formgroup">
                                            <asp:Label ID="Label1" runat="server" CssClass="control-label">
                                                    *Effective From Date:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" CssClass="form-control datepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="control-label">
                                                    *Effective To Date:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="30" CssClass="form-control datepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div runat="server" class=" form-group" id="divReason">
                                            <asp:Label ID="Label5" runat="server" CssClass="control-label">
                                                    *Reason for special timings:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txt_Reason" runat="server" CssClass="form-control"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class=" form-group" id="divStartTime" visible="false">
                                            <asp:Label ID="Label6" runat="server" CssClass="control-label">
                                                    *Start Time:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class="form-group" id="divAbsentTime" visible="false">
                                            <asp:Label ID="Label7" runat="server" CssClass="control-label">
                                                    *Absent Time:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtAbsentTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class=" form-group" id="divEndTime" visible="false">
                                            <asp:Label ID="Label8" runat="server" CssClass="control-label">
                                                    *End Time:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtEndTime" runat="server" CssClass=" form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class=" form-group" id="divMargin" visible="true">
                                            <asp:Label ID="Label9" runat="server" CssClass="control-label">
                                                    *Margin :</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtMargin" runat="server" Text="0" CssClass="form-control"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class=" form-group" id="divFriday" visible="false">
                                            <asp:Label ID="Label10" runat="server" CssClass="control-label">
                                                    *Friday Start Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txtFriStartTime" runat="server" CssClass=" form-control jtimepicker"></asp:TextBox></div>
                                            <asp:Label ID="Label11" runat="server" CssClass="control-label">
                                                    *Friday Absent Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txtFriAbsentTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                            <asp:Label ID="Label12" runat="server" CssClass="control-label">
                                                    *Friday End Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txtFriEndTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class="  form-group" id="divSaturday" visible="false">
                                            <asp:Label ID="Label13" runat="server" CssClass="control-label">
                                                *Saturday Start Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txtSatStartTime" runat="server" CssClass=" form-control jtimepicker"></asp:TextBox></div>
                                            <asp:Label ID="Label14" runat="server" CssClass="control-label">  
                                                  *Saturday Absent Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txtSatAbsentTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                            <asp:Label ID="Label15" runat="server" CssClass="control-label">
                                                    *Saturday End Time:</asp:Label>
                                            <div class="control-label">
                                                <asp:TextBox ID="txtSatEndTime" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                        <div runat="server" class="form-group" id="divhaldDayCB">
                                            <asp:CheckBox runat="server" ID="cbHalfday" CssClass="checkbox" AutoPostBack="true"
                                                Text="Apply Half Day Timings" OnCheckedChanged="cbHalfday_OnCheckedChanged" />
                                        </div>
                                        <div runat="server" class="form-group" id="divhalfDayTime" visible="false">
                                            <asp:Label ID="Label16" runat="server" CssClass="control-label">
                                                *First Half End Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txthfet" runat="server" CssClass=" form-control jtimepicker"></asp:TextBox></div>
                                            <asp:Label ID="Label17" runat="server" CssClass="control-label">  
                                                  *Second Half Start Time:</asp:Label>
                                            <div class=" control-label">
                                                <asp:TextBox ID="txthfst" runat="server" CssClass="form-control jtimepicker"></asp:TextBox></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel_footer">
                                <div class=" form-group pull-right">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSaveSpecialCases_Click"  
                                        CausesValidation="true" CssClass=" btn round_corner" /></div>
                                <div class="form-group pull-right">
                                    <asp:Button ID="btnCancelSpecialCases" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancelSpecialCases_Click" CssClass=" btn round_corner" /></div>
                            </div>
                        </asp:Panel>
                        <div class="panel">
                            <div class="panel_head">
                                Special Cases</div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvShifts" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="table table-hover" EmptyDataText="No record found." HorizontalAlign="Center"
                                    OnPageIndexChanging="gvShifts_PageIndexChanging" OnRowDataBound="gvShifts_RowDataBound"
                                    OnSorting="gvShifts_Sorting" OnRowCommand="gvShifts_RowCommand" AllowPaging="True"
                                    AllowSorting="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeShifts_SpecialCases_Id" HeaderText="VacationTimings_id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee Code">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Full Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DesigName" SortExpression="DesigName" HeaderText="Designation Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Valid_From" SortExpression="Valid_From" HeaderText="Valid From"
                                            DataFormatString="{0: dddd MM/dd/yyyy}"></asp:BoundField>
                                        <asp:BoundField DataField="valid_To" SortExpression="valid_To" HeaderText="Valid To"
                                            DataFormatString="{0: dddd MM/dd/yyyy}"></asp:BoundField>
                                        <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason"></asp:BoundField>
                                        <asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Specific days">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("IsSpecificDays"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("IsSpecificDays"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Show Detail">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkshow" OnClick="btnShow_Click" runat="server" CommandArgument='<%# Eval("EmployeeShifts_SpecialCases_Id") %>'
                                                    CssClass="glyphicon glyphicon-search" ToolTip="View Details" Visible='<%# (int)( Eval("SpecialCase_Type_Id"))==1 %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("EmployeeShifts_SpecialCases_Id") %>'
                                                    CssClass="glyphicon glyphicon-pencil" OnClick="btnEdit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("EmployeeShifts_SpecialCases_Id") %>'
                                                    CssClass="glyphicon glyphicon-trash" OnClick="btnDelete_Click" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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
                        <div class="panel" runat="server" id="Panel_DetailGrid">
                            <div class="panel_head">
                                Detail of Special Cases</div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <asp:GridView ID="gvDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="table table-hover" EmptyDataText="No record found." HorizontalAlign="Center"
                                    AllowPaging="True" AllowSorting="True" PageSize="200">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ActSTime" SortExpression="ActSTime" HeaderText="Start Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActETime" SortExpression="ActETime" HeaderText="End Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AbsentTime" SortExpression="AbsentTime" HeaderText="Absent Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FirStartTime" SortExpression="FirStartTime" HeaderText="Friday Start Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FriAbsentTime" SortExpression="FriAbsentTime" HeaderText="Friday Absent Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FriEndTime" SortExpression="FriEndTime" HeaderText="Friday End Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SatStartTime" SortExpression="SatStartTime" HeaderText="Saturday Start Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SatAbsentTime" SortExpression="SatAbsentTime" HeaderText="Saturday Absent Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SatEndTime" SortExpression="SatEndTime" HeaderText="Saturday End Time">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sunday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Sunday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Sunday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Monday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Monday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Monday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tuesday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Tuesday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Tuesday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wednesday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Wednesday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Wednesday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Thursday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Thursday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Thursday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Friday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Friday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Friday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Saturday">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Visible='<%# Convert.ToBoolean( Eval("Saturday"))==false %>'
                                                    CssClass="glyphicon glyphicon-remove" ForeColor="red"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Visible='<%# Convert.ToBoolean( Eval("Saturday"))==true %>'
                                                    CssClass="glyphicon glyphicon-ok" ForeColor="green"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
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
