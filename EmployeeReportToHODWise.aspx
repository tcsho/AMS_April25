<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeReportToHODWise.aspx.cs" Inherits="EmployeeReportToHODWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="css/examples.css" rel="stylesheet" type="text/css" />--%>
    <%-- <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Employee Reporting</h1>
                    <div class="controls round_corner">
                        <div>
                            <asp:LinkButton ID="btnAddHOD" OnClick="btnAddHOD_Click" runat="server" CssClass="btn round_corner"
                                CausesValidation="False" Font-Bold="False">Add New [HOD]</asp:LinkButton>
                        </div>
                    </div>
                    <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper">
                        <%-- <div class="new_event">--%>
                        <div style="padding: 5px;">
                            <div class="panel_head">
                                <asp:Label ID="lblAddNew" runat="server" Text="Assigning New HOD's"></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddl_region_dept" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_region_dept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="ddl_center_dept" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_center_dept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            *Select Department:
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>
                            <div id="divHODmail" runat="server">
                                <p>
                                    Send Email to HOD :<asp:CheckBox ID="chkEmail" runat="server" /><asp:TextBox ID="txtHODEmail"
                                        runat="server" placeholder="Enter HOD Email" Width="310px"></asp:TextBox></p>
                            </div>
                            <div class="panel" id="div1" runat="server" style="display: inline;">
                                <div class="panel_head">
                                    List of Employees
                                </div>
                                <div class="panel_body">
                                    <asp:GridView ID="gvEmployees" runat="server" Width="100%" AutoGenerateColumns="False"
                                        SkinID="GridView" HorizontalAlign="Center" AllowPaging="True" AllowSorting="True"
                                        PageSize="50" EmptyDataText="No Data Exist!" OnPageIndexChanging="gvEmployees_PageIndexChanging"
                                        OnSorting="gvEmployees_Sorting" OnRowCommand="gvEmployees_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
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
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <asp:DropDownList ID="ddlReportingType" runat="server" Visible="false">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">HOD</asp:ListItem>
                                <asp:ListItem Value="2">SELF</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnSaveHOD" OnClick="btnSaveHOD_Click" runat="server" CssClass="button"
                                ValidationGroup="s" Text="Save"></asp:Button>
                            <asp:Button ID="btnSaveSubordinate" OnClick="btnSaveSubordinate_Click" runat="server"
                                CssClass="button" ValidationGroup="s" Text="Save"></asp:Button>
                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" CausesValidation="False"
                                Text="Cancel"></asp:Button>
                        </div>
                    </asp:Panel>
                    <div class="panel">
                        <div class="panel_head">
                            List of HOD's
                            <br />
                            <br />
                            <div style="clear: both;">
                            </div>
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvHOD" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gvHOD_PageIndexChanging"
                                OnRowDeleting="gvHOD_RowDeleting" OnSorting="gvHOD_Sorting" OnRowDataBound="gvHOD_RowDataBound"
                                AllowPaging="True" AllowSorting="True" PageSize="50" EmptyDataText="No Data Exist!">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HODEmail" HeaderText="HODEmail">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="isEmail" HeaderText="isEmail">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deptCode" HeaderText="deptCode">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                    <asp:BoundField DataField="TotalSubordinates" HeaderText="Total Subordinates" SortExpression="TotalSubordinates" />
                                    <asp:TemplateField HeaderText="Show Subordinates">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnShowSubordinates" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                ImageUrl="~/images/details-icon.png" Width="22px" OnClick="btnShowSubordinates_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add Subordinates">
                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAddSubordinates" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                ImageUrl="~/images/add-icon.png" Width="22px" OnClick="btnAddSubordinate_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/delete.gif" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Region_Id" HeaderText="Region_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Center_Id" HeaderText="Center_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="panel" id="divListOfSubordinates" runat="server" style="display: none;">
                        <div class="panel_head">
                            List of Subordinates
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvSubordinates" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gvSubordinates_PageIndexChanging"
                                OnRowDeleting="gvSubordinates_RowDeleting" OnSorting="gvSubordinates_Sorting"
                                OnRowDataBound="gvSubordinates_RowDataBound" AllowPaging="True" AllowSorting="True"
                                PageSize="50">
                                <Columns>
                                    <asp:BoundField DataField="tid" HeaderText="id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                                    <asp:BoundField DataField="ReportTo" HeaderText="Report To [HOD]" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/delete.gif" Text="" />
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
