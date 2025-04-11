<%@ Page Title="" Language="C#" MasterPageFile="~/AMS.master" AutoEventWireup="true" CodeFile="AttendanceReportsRO.aspx.cs" Inherits="AttendanceReportsRO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="Reoprts_list">
                            <div class="Reoprts_list_body">
                                <h1 class="page_heading">Attendance Reports (RO)
                                </h1>
                                <div class="row">
                                    <div class="Report_criteria  col-md-6">
                                        <div class="Report_criteria_header">
                                            <p>
                                                Select Report Criteria:
                                            </p>

                                        </div>
                                        <div class="Report_criteria_body">


                                            <div class="row ">
                                                <div class="col-xl-12">
                                                    <p>
                                                        Main Organization* :
                                                    </p>
                                                    <asp:DropDownList ID="ddl_MOrg" runat="server" CssClass="dropdownlist"
                                                        Enabled="False" OnSelectedIndexChanged="ddl_MOrg_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="rfv_mOrg" runat="server" Width="200px" Enabled="False"
                                                        ErrorMessage="Mian Org is a required Field" Display="Dynamic" ControlToValidate="ddl_MOrg"
                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator></td>
                                                </div>

                                                <div class="col-xl-12">
                                                    <p>
                                                        Main Organization Country* :
                                                    </p>
                                                    <asp:DropDownList ID="ddl_country" runat="server" CssClass="dropdownlist"
                                                        OnSelectedIndexChanged="ddl_country_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_country" runat="server" Width="165px" Enabled="False"
                                                        ErrorMessage="Country is a required Field" Display="Dynamic" ControlToValidate="ddl_country"
                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-xl-12">
                                                    <div id="trRegion" runat="server">
                                                        <p>
                                                            Region* :
                                                        </p>

                                                        <asp:DropDownList ID="ddl_region" runat="server" CssClass="dropdownlist"
                                                            OnSelectedIndexChanged="ddl_Region_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:RequiredFieldValidator ID="rfv_region" runat="server" Width="169px" Enabled="False"
                                                            ErrorMessage="Region is a required Field" Display="Dynamic" ControlToValidate="ddl_region"
                                                            InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>

                                                <div class="col-xl-12">
                                                    <div id="trCenter" runat="server">
                                                        <p>
                                                            Center*:
                                                        </p>

                                                        <asp:DropDownList ID="ddl_center" runat="server" CssClass="dropdownlist"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddl_center_SelectedIndexChanged">
                                                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_center" runat="server" Width="167px" Enabled="False"
                                                            ErrorMessage="Center is a required Field" Display="Dynamic" ControlToValidate="ddl_center"
                                                            InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>

                                                <div class="col-xl-12">
                                                    <div id="TrMonth" runat="server">
                                                        <p>
                                                            Month* :
                                                        </p>

                                                        <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist">
                                                        </asp:DropDownList></td>


                                                    </div>
                                                </div>


                                                <div class="col-xl-12">
                                                    <div id="trDept" runat="server">
                                                        <p>
                                                            Department* :
                                                        </p>

                                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdownlist">
                                                        </asp:DropDownList></td>


                                                    </div>
                                                </div>




                                                <div class="col-xl-12">
                                                    <div id="trFrmDate" runat="server">
                                                        <p>
                                                            Select Date :
                                                        </p>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" CssClass="datepicker"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-xl-12">
                                                    <asp:Button ID="btnViewReport" runat="server" Text="View Report" CssClass="show_report_btn btn round_corner"
                                                        OnClick="btnViewReport_Click" ValidationGroup="s" />
                                                </div>
                                            </div>

                                        </div>
                                        <!--end Report_criteria_body-->
                                    </div>
                                    <div class="report_names col-md-6">


                                        <asp:RadioButtonList ID="rblReportType" runat="server" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged" AutoPostBack="True">

                                            <asp:ListItem Value="1" Selected="True">Monthly Campus Wise Summary</asp:ListItem>
                                            <asp:ListItem Value="2">Monthly Region Wise Summary</asp:ListItem>
                                            <asp:ListItem Value="3">Monthly Presents Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="4">Monthly Absent Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="5">Monthly Late Arrival Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="6">Monthly Missing-In Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="7">Monthly Missing-Out Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="8">Daily Campus Wise Summary</asp:ListItem>
                                            <asp:ListItem Value="9">Daily Region Wise Summary</asp:ListItem>
                                            <asp:ListItem Value="10">Daily Presents Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="11">Daily Absent Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="12">Daily Late Arrival Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="13">Daily Missing In Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="14">Daily Missing Out Comparison Campus Wise</asp:ListItem>
                                            <asp:ListItem Value="17">Facial Machine Attendance Summary</asp:ListItem>
                                            <asp:ListItem Value="18">Facial Machine Details</asp:ListItem>

                                            <asp:ListItem Value="20">Facial Machine Admin List</asp:ListItem>

                                        </asp:RadioButtonList>




                                    </div>

                                </div>

                            </div>
                            <div class="Reoprts_list_footer">
                                <p class="Show_reoprt_icon">
                                    click to Down
                                </p>
                            </div>
                        </div>
                        <!--end leave_balance-->
                    </div>
                    <!--end inner wrap-->
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

