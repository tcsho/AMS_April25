<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AttendanceReportsHO.aspx.cs" Inherits="AttendanceReportsHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


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

                            <h1 class="page_heading">
                                    Attendance Reports (HO)
                                </h1>


                                <div class="report_names">

                                    <asp:RadioButtonList ID="rblReportType" runat="server" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged" AutoPostBack="True">
                                       <%-- <asp:ListItem Value="15">Attendance Report Summary Detailed</asp:ListItem>--%>
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
                                        <asp:ListItem Value="14">Monthly ERP Leaves Upload</asp:ListItem>
                                    </asp:RadioButtonList>

                                </div>



                                <div class="Report_criteria">
                                    <div class="Report_criteria_header">
                                        <p>Select Report Criteria:</p>
                                
                                    </div>


                                    <div class="Report_criteria_body">
                                        
                                        <div>
                                            <p>
                                                Main Organization* :
                                            </p>
                                            <td valign="top">
                                                <asp:DropDownList ID="ddl_MOrg" runat="server" 
                                                    Enabled="False" OnSelectedIndexChanged="ddl_MOrg_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="rfv_mOrg" runat="server" Width="200px" Enabled="False"
                                                    ErrorMessage="Mian Org is a required Field" Display="Dynamic" ControlToValidate="ddl_MOrg"
                                                    InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                        </div>


                                        <div>
                                            <p>Main Organization Country* :</p>
                                                <asp:DropDownList ID="ddl_country" runat="server"
                                                    OnSelectedIndexChanged="ddl_country_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_country" runat="server" Width="165px" Enabled="False"
                                                    ErrorMessage="Country is a required Field" Display="Dynamic" ControlToValidate="ddl_country"
                                                    InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                        </div>


                                        <div id="trRegion" runat="server">
                                            <p>Region* :</p>
                                                <asp:DropDownList ID="ddl_region" runat="server" 
                                                    OnSelectedIndexChanged="ddl_Region_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_region" runat="server" Width="169px" Enabled="False"
                                                    ErrorMessage="Region is a required Field" Display="Dynamic" ControlToValidate="ddl_region"
                                                    InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                        </div>


                                        <div id="trCenter" runat="server">
                                            
                                                <p><asp:Label ID="lab_center" runat="server" Text="Center* : "></asp:Label></p>
                                            
                                                <asp:DropDownList ID="ddl_center" runat="server" 
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddl_center_SelectedIndexChanged">
                                                </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_center" runat="server" Width="167px" Enabled="False"
                                                    ErrorMessage="Center is a required Field" Display="Dynamic" ControlToValidate="ddl_center"
                                                    InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator>
                                        </div>

                                        <div id="Tr1" runat="server">

                                                <p><asp:Label ID="Label1" runat="server" Text="Month* : "></asp:Label></p>

                                                <asp:DropDownList ID="ddlMonths" runat="server">
                                                </asp:DropDownList>
                                        </div>
                                          <div id="divDept" runat="server">

                                                <p><asp:Label ID="Label2" runat="server" Text="Department* : "></asp:Label></p>

                                                <asp:DropDownList ID="ddlDepartment" runat="server">
                                                </asp:DropDownList>
                                        </div>
                                        <div id="trFrmDate" runat="server">
                                            <p>
                                                From Date :
                                            </p>
                                            <td valign="top">
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="datepicker"></asp:TextBox>
                                                <cc1:CalendarExtender
                                                    ID="CalendarExtender1" runat="server" TargetControlID="txtFrmDate">
                                                </cc1:CalendarExtender>
                                            
                                        </div>

                                        <asp:Button ID="Button1" runat="server" Text="View Report" CssClass="btn"
                                        OnClick="btnViewReport_Click" ValidationGroup="s" /></td>

                                    </div>


                                </div>

                        </div>
                    </div>

                    
                
                </div>


            
            </div>
        
        </div>








        <%--<div class="inner_wrap">
        <div class="Reoprts_list_body">
        <table class="main_table" cellspacing="0" cellpadding="0" width="100%" align="center"
                        border="0">
                        <tbody>
                            <tr>
                                <td align="right" colspan="12">
                                    <asp:Button ID="btnViewReport" runat="server" Text="View Report" Width="92px" CssClass="show_report_btn btn round_corner"
                                        OnClick="btnViewReport_Click" ValidationGroup="s" /></td>
                            </tr>
                            <tr id="crt" runat="server">
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" align="center" border="0">
                                        
                                        <tr style="height:5px">
                                            <td colspan="2">
                                            
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="titlesection" align="left" colspan="2">
                                                Selection Criteria</td>
                                        </tr>
                                        <tr style="height:5px">
                                            <td colspan="2">
                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="border:solid 1px silver;" class="reportCatPanel">
                                            
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
                                                    
                                                </asp:RadioButtonList>
                                            
                                            </td>
                                            
                                            <td style="vertical-align:top" class="reportCriteriaPanel">
                                               <div class="Report_criteria_body">
                                               
                                                        <table>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td style="width: 217px" valign="top" align="right">
                                                                    Main Organization* :
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddl_MOrg" runat="server" CssClass="dropdownlist" Width="250px"
                                                                        Enabled="False" OnSelectedIndexChanged="ddl_MOrg_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="rfv_mOrg" runat="server" Width="200px" Enabled="False"
                                                                        ErrorMessage="Mian Org is a required Field" Display="Dynamic" ControlToValidate="ddl_MOrg"
                                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 12px">
                                                                </td>
                                                                <td align="right" style="width: 217px" valign="top">
                                                                    Main Organization Country* :</td>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddl_country" runat="server" CssClass="dropdownlist" Width="250px"
                                                                        OnSelectedIndexChanged="ddl_country_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_country" runat="server" Width="165px" Enabled="False"
                                                                        ErrorMessage="Country is a required Field" Display="Dynamic" ControlToValidate="ddl_country"
                                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator></td>
                                                            </tr>
                                                            <tr id="trRegion" runat="server">
                                                                <td style="width: 12px; height: 18px">
                                                                </td>
                                                                <td style="width: 217px; height: 18px" valign="top" align="right">
                                                                    Region* :
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddl_region" runat="server" CssClass="dropdownlist" Width="250px"
                                                                        OnSelectedIndexChanged="ddl_Region_SelectedIndexChanged" AutoPostBack="True">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="rfv_region" runat="server" Width="169px" Enabled="False"
                                                                        ErrorMessage="Region is a required Field" Display="Dynamic" ControlToValidate="ddl_region"
                                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator></td>
                                                                <td valign="top" align="right">
                                                                    </td>
                                                            </tr>
                                                            <tr id="trCenter" runat="server">
                                                                <td style="width: 12px; height: 18px">
                                                                </td>
                                                                <td align="right" style="width: 217px; height: 18px" valign="top">
                                                                    <asp:Label ID="lab_center" runat="server" Text="Center* : "></asp:Label></td>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddl_center" runat="server" CssClass="dropdownlist" Width="250px"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_center_SelectedIndexChanged">
                                                                    </asp:DropDownList><asp:RequiredFieldValidator ID="rfv_center" runat="server" Width="167px" Enabled="False"
                                                                        ErrorMessage="Center is a required Field" Display="Dynamic" ControlToValidate="ddl_center"
                                                                        InitialValue="0" ValidationGroup="s"></asp:RequiredFieldValidator></td>
                                                                <td align="right" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr1" runat="server">
                                                                <td style="width: 12px; height: 18px">
                                                                </td>
                                                                <td align="right" style="width: 217px; height: 18px" valign="top">
                                                                    <asp:Label ID="Label1" runat="server" Text="Month* : "></asp:Label></td>
                                                                <td valign="top">
                                                                    <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist" Width="250px">
                                                                    </asp:DropDownList></td>
                                                                <td align="right" valign="top">
                                                                </td>
                                                            </tr>
                                                            <tr id="trFrmDate" runat="server">
                                                                <td style="width: 12px; height: 18px">
                                                                </td>
                                                                <td style="width: 217px; height: 18px" valign="top" align="right">
                                                                    From Date :
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" Width="125px"></asp:TextBox><cc1:CalendarExtender
                                                                        ID="CalendarExtender1" runat="server" TargetControlID="txtFrmDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td valign="top" align="right">
                                                                </td>
                                                            </tr>
                                                        </table>
                                               </div>
                                                    
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>




        </div>

            <div class="Reoprts_list_footer">
            	<p class="Show_reoprt_icon">click to Down</p>
            </div>
            

</div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

