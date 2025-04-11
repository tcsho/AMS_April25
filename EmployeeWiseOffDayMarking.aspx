<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeWiseOffDayMarking.aspx.cs" Inherits="EmployeeWiseOffDayMarking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style>
        .report_names input[type='checkbox']
        {
            -webkit-appearance: checkbox;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Employee(s) Off Days</h1>
                    <div class="panel" id="ShowEmployee" runat="server">
                        <div class="panel_head">
                            <%--  List of off days employee(s) wise--%>
                            <asp:Button ID="btnSingleEmpProcess" runat="server" CausesValidation="False" CssClass=" btn round_corner"
                                OnClick="btnSingleEmpProcess_Click" Text="Add New" Width="130px" />
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvEmployeeOff" runat="server" Width="100%" AutoGenerateColumns="False"
                                HorizontalAlign="Center">
                                <Columns>
                                    <asp:BoundField DataField="EWO_Id" HeaderText="EWO_Id"></asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                    <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="FromDate" />
                                    <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="ToDate" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnRemove" runat="server" CommandArgument='<%# Eval("EWO_Id") %>'
                                                OnClick="btnRemove_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                ToolTip="Remove" ImageUrl="~/images/delete.gif"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label2" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>
                    <div class="Reoprts_list" id="ShorForm" runat="server">
                        <div class="Reoprts_list_body">
                            <div class="report_names" id="div_WeekDays" runat="server">
                                <asp:CheckBoxList ID="cblWeekdays" runat="server" Width="100%">
                                    <asp:ListItem Value="Sunday">Sunday</asp:ListItem>
                                    <asp:ListItem Value="Monday">Monday</asp:ListItem>
                                    <asp:ListItem Value="Tuesday">Tuesday</asp:ListItem>
                                    <asp:ListItem Value="Wednesday">Wednesday</asp:ListItem>
                                    <asp:ListItem Value="Thursday">Thursday</asp:ListItem>
                                    <asp:ListItem Value="Friday">Friday</asp:ListItem>
                                    <asp:ListItem Value="Saturday">Saturday</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="Report_criteria">
                                <div class="Report_criteria_header">
                                    <%--<p>
                                    Select Report Criteria:</p>--%>
                                    <asp:RadioButtonList ID="rbLstOpt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbLstRpt_SelectedIndexChanged"
                                        Width="100%">
                                        <asp:ListItem Value="0" Selected="True">Date Range</asp:ListItem>
                                        <asp:ListItem Value="1">Week Days</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <br>
                                </div>
                                <div class="Report_criteria_body">
                                    <p>
                                        <asp:Label ID="error" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></p>
                                    <p>
                                        From Date:</p>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date" CssClass="datepicker"></asp:TextBox>
                                    <p>
                                        To Date:</p>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date" CssClass="datepicker"></asp:TextBox>
                                    <p>
                                        Off Days Reason:</p>
                                    <asp:TextBox TextMode="MultiLine" ID="txtOffDaysReason" runat="server" placeholder="Off Days Reason"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel" id="ShorForm1" runat="server">
                        <div class="panel_head">
                            List Of Selected Employee(s)
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_EmpApply" runat="server" Width="100%" AutoGenerateColumns="False"
                                HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                OnClick="btnDel_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                ToolTip="Remove" ImageUrl="~/images/delete.gif"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="lab_dataStatus" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                        <div>
                            <asp:Button ID="btn_apply" runat="server" CssClass="btn" ValidationGroup="s" Text="Apply"
                                OnClick="btn_apply_Click"></asp:Button>
                            <asp:Button ID="but_cancel" runat="server" CssClass="btn" CausesValidation="False"
                                Text="Cancel" OnClick="but_cancel_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="panel" id="ShorForm2" runat="server">
                        <div class="panel_head">
                            Search & Add Employee(s)
                        </div>
                        <hr />
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:LinkButton ID="but_Add" runat="server" CssClass="btn round_corner" CausesValidation="False"
                                OnClick="but_Add_Click">Add Employee(s)</asp:LinkButton>
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvEmployee" runat="server" Width="100%" AutoGenerateColumns="False"
                                HorizontalAlign="Center" OnRowCommand="gvMIO_RowCommand" EmptyDataText="No Data Exist!">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="DesigName" SortExpression="DesigName" />
                                    <asp:TemplateField HeaderText="cb">
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbSelectEmployee" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label1" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
