<%@ Page Title="" Language="C#" MasterPageFile="~/AMS.master" AutoEventWireup="true"
    CodeFile="EmployeeReportTo.aspx.cs" Inherits="EmployeeReportTo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript" charset="utf-8">

$(document).ready(function() {
    var table=$('table.datatable').DataTable(
        {
destroy: true,
"aLengthMenu": [[10, 25, 50, 100,150, 200, -1], [10, 25, 50, 100,150, 200, "All"]]
            , "iDisplayLength": 150
            , 'bLengthChange': true
            , "order": [[0, "asc"]]
            , "paging": true
            , "ordering": true
            , "searching": true
            , "info": true
            , "scrollX": true
            , "scrollY": true
            ,"responsive":true
        }

    );



} );
</script>


            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">Employee Reporting</h1>
                    <div class="controls round_corner">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlEmployeecode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeecode_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="s"
                            Display="Dynamic" ErrorMessage="Select Employee" ControlToValidate="ddlEmployeecode"
                            InitialValue="0"></asp:RequiredFieldValidator>
                        <asp:LinkButton ID="but_new" OnClick="but_new_Click1" runat="server" CssClass="btn round_corner" Visible="false"
                            CausesValidation="False">Add New</asp:LinkButton>
                    </div>
                    <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper">
                        <div class="new_event">
                            <p>
                                <asp:Label ID="error" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                            </p>
                            <p>
                                Select HOD *:
                            </p>
                            <asp:DropDownList ID="ddlHOD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHOD_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="s"
                                Display="Dynamic" ErrorMessage="Select HOD" ControlToValidate="ddlHOD" InitialValue="0"></asp:RequiredFieldValidator>
                            <p>
                                HOD Email:
                            </p>
                            <asp:TextBox ID="txtHODEmail" runat="server" Placeholder="Enter HOD email"></asp:TextBox>
                            <p>
                                Send Email to HOD :
                                <asp:CheckBox ID="chkEmail" runat="server" />
                            </p>
                            <p>
                                Reporting Type:
                            </p>
                            <asp:DropDownList ID="ddlReportingType" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">HOD</asp:ListItem>
                                <asp:ListItem Value="2">SELF</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Button ID="but_save" OnClick="but_save_Click" runat="server" CssClass="button"
                                ValidationGroup="s" Text="Save"></asp:Button>
                            &nbsp;
                            <asp:Button ID="but_cancel" OnClick="but_cancel_Click" runat="server" CssClass="button"
                                CausesValidation="False" Text="Cancel"></asp:Button>
                        </div>
                    </asp:Panel>


                    <div class="panel">
                        <div class="panel_head">
                            Employee Reporting
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="dv_country" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="dv_country_PageIndexChanging"
                                OnRowDeleting="dv_country_RowDeleting" OnSorting="dv_country_Sorting" OnRowDataBound="dv_country_RowDataBound"
                                OnSelectedIndexChanging="dv_country_SelectedIndexChanging" AllowPaging="True"
                                AllowSorting="True" PageSize="50" CssClass="datatable table table-striped table-bordered table-hover display" >
                                <Columns>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tid" HeaderText="id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="HODEmail" HeaderText="Pid">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="isEmail" HeaderText="Pmid">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="HODEmployeeCode" HeaderText="Employee Code"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="ReportingType" HeaderText="Reporting" SortExpression="ReportingType" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="Edit/Delete">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="Update" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" CausesValidation="False" CommandName="Select"
                                                ImageUrl="~/images/edit.gif" Text="Edit" />
                                            &nbsp;
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
