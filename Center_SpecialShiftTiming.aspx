<%@ Page Title="Center Special Shift Timing" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Center_SpecialShiftTiming.aspx.cs" Inherits="Center_SpecialShiftTiming" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <link rel="stylesheet" type="text/css" href="css/jquery.dataTables.css">
            <script type="text/javascript" charset="utf8" src="Scripts/jquery.dataTables.min.js">
            </script>
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/jquery.timepicker.css" rel="Stylesheet" type="text/css" />
            <script src="Scripts/jquery.timepicker.js" type="text/javascript"></script>
            <script type="text/javascript">



                $(document).ready(document_Ready);

                function document_Ready() {


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
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="form-group">
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                            </div>
                            <div class=" col-lg-6 col-md-6 col-sm-6 col-xs-12 display-2">
                                <p class="page_heading">
                                    Special Shift Timing for Center</p>
                            </div>
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                                <br />
                            </div>
                        </div>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px" Enabled="false" AutoPostBack="True">
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
                                        <asp:GridView ID="gvCenter" runat="server" Width="100%" AutoGenerateColumns="False"  OnRowDataBound="gvCenters_RowDataBound"
                                            HorizontalAlign="Center" SkinID="GridView" BackColor="white" CssClass="table table-hover"   OnRowCommand="gvCenters_RowCommand" AllowPaging="True" AllowSorting="True"
                                        PageSize="1000" OnPageIndexChanging="gvCenter_PageIndexChanging" OnSorting="gvCenter_Sorting"
                                    HeaderStyle-BackColor="#c6c6c6"  >
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
                                                <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name">
                                                </asp:BoundField>
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
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Attendance Date:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtAttDate" runat="server" class="datepicker" placeholder="Attendance Date"
                                                        CausesValidation="true">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttDate"
                                                        Display="Dynamic" ErrorMessage="Attendance Date Required" SetFocusOnError="True"
                                                        ForeColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblStart" runat="server" for="Start Time : " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtStart" runat="server" CssClass="jtimepicker" placeholder="Start Time"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!--end form group-->
                                            <div class="form-group">
                                                <asp:Label ID="lblEnd" runat="server" for="End Time: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtEnd" runat="server" CssClass="jtimepicker" AutoPostBack="true"
                                                        placeholder="End Time">
                                                    </asp:TextBox>
                                                    <%-- <asp:label  runat="server" ID="trNoteTime"  Visible="false" ForeColor="white" Text="Time entered is not valid "  ></asp:label>--%>
                                                </div>
                                            </div>
                                            <!--end form group-->
                                            <div class="form-group">
                                                <asp:Label ID="lblMargin" runat="server" for="Margin: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Margin (Minutes):</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtMargin" runat="server" placeholder="Margin" CausesValidation="true">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter an Integer!"
                                                        ControlToValidate="txtMargin" ValidationExpression="^\d+" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="white">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblAbsent" runat="server" for="Absent Time: " ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Absent Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtAbsent" runat="server" CssClass="jtimepicker" placeholder="Absent Time ">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblTchStart" runat="server" for="Teacher Start Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher Start Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txttchStart" runat="server" CssClass="jtimepicker" placeholder="Teacher Start Time "></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblTchEnd" runat="server" for="Teacher End Time: " ForeColor="white"
                                                    class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Teacher End Time:</asp:Label>
                                                <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                                                    <asp:TextBox ID="txtTchEnd" runat="server" CssClass="jtimepicker" placeholder="Teacher End Time "
                                                        AutoPostBack="True"></asp:TextBox>
                                                    <%-- <asp:label  runat="server" ID="trNoteteacher"  Visible="false" ForeColor="white" Text="Time entered is not valid "></asp:label>--%>
                                                </div>
                                            </div>
                                            <!--end form group-->
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
                                            <!--end form group-->
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
                                    List of Campuses with Special Time</div>
                                <div class="panel-body">
                                    <asp:GridView ID="gvDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                        OnPageIndexChanging="gvDetails_PageIndexChanging" HorizontalAlign="Center" SkinID="GridView"
                                        BackColor="white" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6"
                                        EmptyDataText="No Data Exist for the current month!" AllowPaging="True" AllowSorting="True"
                                        PageSize="20" OnSorting="gvDetails_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="CenterShifts_SpecialCases_ID" HeaderText="" SortExpression="CenterShifts_SpecialCases_ID">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AttDate" HeaderText="Attendance Date" SortExpression="AttDate"
                                                DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                                            <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StartTime" HeaderText="Start Time" SortExpression="StartTime" />
                                            <asp:BoundField DataField="EndTime" HeaderText="End Time" SortExpression="EndTime" />
                                            <asp:BoundField DataField="Margin" HeaderText=" Margin  " SortExpression="Margin" />
                                            <asp:BoundField DataField="AbsentTime" HeaderText=" AbsentTime" SortExpression="AbsentTime" />
                                            <asp:BoundField DataField="TchrSTime" HeaderText="Teacher Start Time" SortExpression="TchrSTime" />
                                            <asp:BoundField DataField="TchrETime" HeaderText="Teacher End Time" SortExpression="TchrETime" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                                            <asp:TemplateField ShowHeader="False" HeaderText="Update Details">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdateDetails" runat="server" CommandArgument='<%# Eval("CenterShifts_SpecialCases_ID") %>'
                                                        CausesValidation="False" CommandName="Edit Details" OnClick="btnEdit_Click" ImageUrl="~/images/approval.png"
                                                        Text="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("CenterShifts_SpecialCases_ID") %>'
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
