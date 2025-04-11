<%@ Page Title="" Language="C#" MasterPageFile="~/AMS.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Repoprts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <%-- <link href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css"  
        rel="stylesheet" type="text/css" />  --%>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
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
        $(document).ready(function () {
            $('#lstEmployee').click(function () {
                $("#lstEmployee option:selected").css("background-color", "#e0e0e0");
            });
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="Reoprts_list">
                            <div class="Reoprts_list_body">
                                <h1 class="page_heading">Attendance System Reports
                                </h1>

                                <div class="row">

                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-xl-12">
                                                <div class="Report_criteria">

                                                    <div class="Report_criteria_header">
                                                        <p>
                                                            Select Report Criteria:
                                                        </p>
                                                        <asp:RadioButton ID="rbMonth" runat="server" AutoPostBack="True" Checked="true" GroupName="a"
                                                            OnCheckedChanged="rbMonth_CheckedChanged" Text="Monthly" />
                                                        <asp:RadioButton ID="rbRange" runat="server" AutoPostBack="True" GroupName="a" OnCheckedChanged="rbRange_CheckedChanged"
                                                            Text="Date Ranges" />
                                                        <br>
                                                    </div>
                                                    <div class="row">
                                                    <div class="Report_criteria_body">
                                                        <div id="trMonth" runat="server" class="form-group col-xl-12">
                                                            <p>
                                                                Month:
                                                            </p>
                                                            <asp:DropDownList ID="ddlMonths" runat="server" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged"
                                                                AutoPostBack="true" >
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br>
                                                        <div>
                                                            <div id="trFrmDate" runat="server" visible="false"  class="form-group col-xl-12">
                                                                <p>
                                                                    FromDate:
                                                                </p>
                                                                <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" CssClass="datepicker"></asp:TextBox>
                                                            </div>
                                                            <div id="trToDate" runat="server" visible="false"  class="form-group col-xl-12">
                                                                <p>
                                                                    ToDate:
                                                                </p>
                                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="30" CssClass="datepicker"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="div_region" runat="server" visible="false"  class="form-group col-xl-12">

                                                            <p>
                                                                Region:
                                                            </p>
                                                            <asp:DropDownList ID="ddl_region" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="div_center" runat="server" visible="false"  class="form-group col-xl-12">

                                                            <p>
                                                                Center:
                                                            </p>
                                                            <asp:DropDownList ID="ddl_center" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_center_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="TrDept" runat="server" visible="false"  class="form-group col-xl-12">
                                                            <p>
                                                                Department:
                                                            </p>
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div  class="form-group col-xl-12">
                                                             <p>
                                                            <asp:CheckBox ID="chkemp" runat="server" AutoPostBack="True" OnCheckedChanged="chkemp_CheckedChanged"
                                                                Text="Single Employee" />
                                                        </p>

                                                        </div>
                                                       
                                                        <div id="trsingleemp" runat="server" visible="false"  class="form-group col-xl-12">
                                                            <p>
                                                                Single Employee:
                                                            </p>
                                                            <asp:DropDownList ID="ddlEmployeecode" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="trMultiEmp" runat="server" visible="false"  class="form-group col-xl-12">
                                                            <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple"
                                                                Height="250px" OnSelectedIndexChanged="lstEmployee_SelectedIndexChanged" CssClass="selectedRow"></asp:ListBox>
                                                        </div>
                                                        <asp:Button ID="btnViewReport" runat="server" CssClass="btn" OnClick="btnViewReport_Click"
                                                            Text="View" />
                                                        <asp:Button runat="server" ID="btnCafeReport" Text="View Report" Visible="false"
                                                            CssClass="btn" OnClick="btnCafeReport_Click" />
                                                    </div>
                                                    <!--end Report_criteria_body-->
                                                        </div>
                                                </div>
                                            </div>

                                        </div>


                                    </div>
                                    <div class="col-md-6">
                                        <div class="report_names">
                                            <asp:RadioButtonList ID="rbLstRpt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbLstRpt_SelectedIndexChanged"
                                                Width="100%">
                                                <asp:ListItem Value="0">Monthly Attendance Report</asp:ListItem>
                                                <asp:ListItem Value="1">Attendance Log Report</asp:ListItem>
                                                <asp:ListItem Value="2">Absent Report</asp:ListItem>
                                                <asp:ListItem Value="3">Reds Attendance Report</asp:ListItem>
                                                <asp:ListItem Value="4">Employee Leave Balance Report</asp:ListItem>
                                                <asp:ListItem Value="5">Missing In Out Report</asp:ListItem>
                                                <%--<asp:ListItem Value="6">Break In/Out Report</asp:ListItem>--%>
                                                <asp:ListItem Value="7">Attendance Report Department Wise Summary</asp:ListItem>
                                                <asp:ListItem Value="9">Attendance Report Summary</asp:ListItem>
                                                <asp:ListItem Value="8">Attendance Analysis</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RadioButtonList ID="rblCafe" runat="server" AutoPostBack="True" Visible="false"
                                                OnSelectedIndexChanged="rblCafe_SelectedIndexChanged" Width="100%">
                                                <asp:ListItem Value="0">Cafe Log Employee Wise </asp:ListItem>
                                                <asp:ListItem Value="1">Cafe Log Date Wise</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

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
