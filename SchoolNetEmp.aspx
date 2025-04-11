<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SchoolNetEmp.aspx.cs" Inherits="SchoolNetEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
            <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
            <script type="text/javascript" src="//cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
            </script>
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

                                    //    { orderable: false, targets: [4]} //disable sorting on toggle button
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
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="form-group">
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                            </div>
                            <div class=" col-lg-6 col-md-6 col-sm-6 col-xs-12 display-2">
                                <p class="page_heading">
                                    Network Employees in School</p>
                            </div>
                            <div class=" col-lg-3 col-md-3 col-sm-3 col-xs-12 display-2">
                                <br />
                            </div>
                        </div>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="false" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist"  
                                AutoPostBack="True" Visible="false" >
                            </asp:DropDownList>
                        </div>
                        <asp:Panel runat="Server" ID="AddNew" class="panel">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <asp:GridView ID="gvNetwork" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnPreRender="gvNetwork_PreRender" HorizontalAlign="Center" SkinID="GridView"
                                            CssClass="datatable table table-hover">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Region_Name" HeaderText="Region Name" SortExpression="Region_Name">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="location" HeaderText="Location" SortExpression="location">
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Change Location To">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSchool" runat="server" CausesValidation="False" OnClick="lnkLocation_Click"
                                                            Visible='<%# (int)(Eval("inSchool"))==0  %>' CommandArgument="True">School </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkRegion" runat="server" CausesValidation="False" OnClick="lnkLocation_Click"
                                                            Visible='<%# (int)(Eval("inSchool"))==1  %>' CommandArgument="False">Reigonal Office</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
