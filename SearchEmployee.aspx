<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="SearchEmployee.aspx.cs" Inherits="SearchEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
            <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
            <script type="text/javascript" src="//cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
            <script type="text/javascript">

                Sys.Application.add_init(function () {
                    // Initialization code here, meant to run once.
                    $.noConflict();
                    jQuery(document).ready(document_Ready);


                    function document_Ready() {

                        jQuery(document).ready(function () {

                            //****************************************************************


                            try {
                                jQuery('table.datatable').DataTable({
                                    destroy: true,
                                    // sDom: 'T<"dataTables_wrapper"tfrlip>', // its ok


                                    //                    dom: "<'row'<'col-sm-5'T><'col-sm-7'f>>R" +
                                    dom: "<'row'<'col-sm-4'l><'col-sm-3'T><'col-sm-5'f>>R" +
                                        "<'row'<'col-sm-12'tr>>" +
                                        //                     "<'row'<'col-sm-12'l>>" +
                                        "<'row'<'col-sm-12'i>><'row'<'col-sm-12'p>>",
                                    "columnDefs": [

                                        //   { orderable: false, targets: [4]} //disable sorting on toggle button
                                    ]
                                    , "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]], "iDisplayLength": 25, 'bLengthChange': true // ,"bJQueryUI":true , fixedHeader: true 
                                    , "order": [[0, "asc"]], "paging": true, "ordering": true, "searching": true, "info": true, "scrollX": false, "stateSave": true
                                    , //--- Dynamic Language---------
                                    "oLanguage": {
                                        "sZeroRecords": "There are no Records that match your search critera",
                                        //                    "sLengthMenu": "Display _MENU_ records per page&nbsp;&nbsp;",
                                        "sInfo": "Displaying _START_ to _END_ of _TOTAL_ records",
                                        "sInfoEmpty": "Showing 0 to 0 of 0 records",
                                        "sInfoFiltered": "(filtered from _MAX_ total records)",
                                        "sEmptyTable": 'No Rows to Display.....!',
                                        "sSearch": "Search :"
                                    }
                                }
                                );
                            }
                            catch (err) {
                                alert('datatable ' + err);
                            }

                            //****************************************************************



                        }
                        );

                    } //end of documnet_ready()



                    //Re-bind for callbacks
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(function () {
                        jQuery(document).ready(document_Ready);
                        //            document_Ready();
                        //            alert('call back done');
                    }
                    );

                });
            </script>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <asp:Panel ID="Panel70" runat="server" CssClass="Pbar">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" />
                        </div>
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">Search Employee</h1>
                    <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper">
                        <%-- <div class="new_event">--%>
                        <div style="padding: 5px;">
                            <div class="panel_head">
                                <br />
                                <br />
                                <div style="clear: both;">
                                </div>
                                <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlCenter" runat="server" Width="323px">
                                </asp:DropDownList>
                                <br />
                                <div style="clear: both;">
                                </div>
                            </div>
                            <div class="panel-body" style="text-align: right">
                                <table>
                                    <tr>
                                        <td style="width: 200px; color: White">Employee Code :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" Width="200px" placeholder="Type Employee Code here..."
                                                AutoPostBack="True" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                                        </td>
                                        <td style="width: 200px; color: White">Employee Name :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" Width="200px" placeholder="Type Employee Name here..."
                                                AutoPostBack="True" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                        </td>
                                        <td style="width: 200px; color: White">Designation :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlDesignation" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px; color: White">Department :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlDept" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 200px; color: White">Grade :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlGrade" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 200px; color: White">Religion :
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlReligion" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px; color: White">Active/Resigned :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlInActive" runat="server" Width="200px">
                                                <asp:ListItem Value="-1">-- All -- </asp:ListItem>
                                                <asp:ListItem Selected="True" Value="n">Active</asp:ListItem>
                                                <asp:ListItem Value="y">Resigned</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 200px; color: White">Permanent/Contractual :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlIsContracual" runat="server" Width="200px">
                                                <asp:ListItem Value="-1">-- All -- </asp:ListItem>
                                                <asp:ListItem Value="0">Permanent</asp:ListItem>
                                                <asp:ListItem Value="1">Contractual</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 200px; color: White">Gender :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlGender" runat="server" Width="200px">
                                                <asp:ListItem Value="-1">-- All -- </asp:ListItem>
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div style="clear: both;">
                                    &nbsp
                                </div>
                                <div class="col-lg-6">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="pull-right btn btn-default" Width="100px"
                                        OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" Width="100px"
                                        OnClick="btnReset_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div id="loading" runat="server" style="text-align: center; display: none">
                        <asp:Image ID="imgLoading" Width="150px" Height="150px" ImageUrl="~/images/loading30.gif"
                            runat="server" />
                    </div>
                    <div class="panel" id="divListOfSearchEmployee" runat="server" visible="false" style="overflow-x: auto;">

                        <div>

                            <asp:GridView ID="gvSearchEmployee" runat="server" Width="100%" AutoGenerateColumns="False" SkinID="GridView"
                                CssClass="datatable table" HorizontalAlign="Center" OnPreRender="gvSearchEmployee_PreRender">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Region_Name" HeaderText="Region Name" SortExpression="EmployeeCode"></asp:BoundField>
                                    <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="EmployeeCode"></asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="Department" />
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode"></asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="Designation" />
                                    <asp:BoundField DataField="EmpGrade" HeaderText="Grade" SortExpression="Grade" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                    <asp:BoundField DataField="Religion_Name" HeaderText="Religion" SortExpression="Gender" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" SortExpression="MobileNo" />
                                    <asp:BoundField DataField="ExtensionNo" HeaderText="Extension No" SortExpression="ExtensioNo" />
                                    <asp:TemplateField HeaderText="Contractual">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("isContractual")%>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cb" runat="server" Checked='<%# Eval("InActive").ToString() == "n " ? true : false %>'
                                                Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Log Report">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrintLog" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    OnClick="btnPrintLog_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print Report" ImageUrl="~/images/Printer.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Attendance Report">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrintAtt" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    OnClick="btnPrintAtt_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print Report" ImageUrl="~/images/Printer.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
