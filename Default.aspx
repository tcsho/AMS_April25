<%@ Page Title="" Language="C#" MasterPageFile="~/AMS.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<script type="text/javascript" charset="utf-8">

    $(document).ready(
        function () {
            TableData();
        }
    );

    function TableData() {
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
    }



</script>

            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Employee Summary
                        </p>
                        <div class="controls">
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist" Width="130px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>

                            <asp:Button runat="server" ID="btn_viewPendingApprovals"
                                Text="Pending Submission & Approvals"
                                OnClick="btn_viewPendingApprovals_Click" CssClass=" btn round_corner " Visible="false" />

                            <asp:Button runat="server" ID="btn_PendingAttSummary"
                                Text="Pending Attendance Summary"
                                OnClick="btn_PendingAttSummary_Click" CssClass=" btn round_corner " Visible="false" />

                            <asp:Button runat="server" ID="btn_pendingAtttendanceRegionWise"
                                Text="Pending Attendance Region Wise"
                                OnClick="btn_pendingAtttendanceRegionWise_Click" CssClass=" btn round_corner " Visible="false" />

                        </div>

                        <div class="panel">
                            <div class="panel_head">
                                Employee Summary
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body" style="overflow: auto; max-height: 100%">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvLib" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" 
                                    AllowPaging="True" AllowSorting="True" PageSize="300"
                                    EmptyDataText="No Data Exist!" OnRowDataBound="gvLib_RowDataBound"
                                     CssClass="datatable table table-striped table-bordered table-hover display"

                                    >
                                    <Columns>
    
                                        <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="EMP #">
                                           <ItemStyle HorizontalAlign="Left"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fullname" SortExpression="fullname" HeaderText="Name">
                                            <ItemStyle  CssClass="ColumnSeptrator" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Absents" SortExpression="Absents" HeaderText="Total Absents">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Black"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Submitted" SortExpression="Submitted" HeaderText="Leaves Submitted">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Black"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Approved" SortExpression="Approved" HeaderText="Leaves Approved">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Black"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UnApproved" SortExpression="UnApproved" HeaderText="Leaves Unapproved">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Red"
                                                Font-Size="Medium" Font-Bold="True" CssClass="ColumnSeptrator" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LateArrival" SortExpression="LateArrival" HeaderText="Late Arrivals">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Green"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LateSubmitted" SortExpression="LateSubmitted" HeaderText="Late Arrivals Submitted">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Green"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LateApproved" SortExpression="LateApproved" HeaderText="Late Arrivals Approved">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Green"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LateUnApproved" SortExpression="LateUnApproved" HeaderText="Late Arrivals Unapproved">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Red"
                                                Font-Size="Medium" Font-Bold="True"  CssClass="ColumnSeptrator" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Missing_Attendance" SortExpression="Missing_Attendance"
                                            HeaderText="Missing">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Blue"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MissingSubmitted" SortExpression="MissingSubmitted" HeaderText="Missing Submitted">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Blue"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MissingApproved" SortExpression="MissingApproved" HeaderText="Missing Approved">
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Blue"
                                                Font-Size="Medium" Font-Bold="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MissingUnApproved" SortExpression="Missing Unapproved"
                                            HeaderText="UnApvd" >
                                            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="Red"
                                                Font-Size="Medium" Font-Bold="True"  CssClass="ColumnSeptrator"  />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Reset Pass">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPassReset" runat="server" CausesValidation="false" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    ImageUrl="~/images/passrest.png" OnClick="btnPassChange_Click" OnClientClick="return confirm('Are you sure to perform this action?')" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Att Report">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    OnClick="btnPrint_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print Report" ImageUrl="~/images/Printer.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approve" Visible="false">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnApp" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    OnClick="btnApp_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print Quotations" ImageUrl="~/images/approval.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <!--end panel_footer-->
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
