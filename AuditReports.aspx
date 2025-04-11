<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AuditReports.aspx.cs" Inherits="AuditReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>



        <div class="inner_wrap">
        <div class="Reoprts_list_body">

        <asp:Button ID="btnViewReport" class="button" runat="server" Text="View" CssClass="show_report_btn btn round_corner"
                                                    Width="58px" OnClick="btnViewReport_Click" />
           <table cellpadding="0" cellspacing="0" width="100%">
                        
                        
                        <tr class="tr2">
                        <td style="width:30%;color:#FFF" align="left" valign="top" >
                            <asp:RadioButtonList BorderWidth="2px"  BorderColor="silver" Width="100%" ID="rbLstRpt" runat="server" >
                                <asp:ListItem Value="0">Report with 2 or lesser Reds</asp:ListItem>
                                <asp:ListItem Value="1">Report 3 or more Reds</asp:ListItem>
                                <asp:ListItem Value="2">Approved Missing Entries</asp:ListItem>
                                <asp:ListItem Value="3">Un-Approved Missing Entries</asp:ListItem>
                                <asp:ListItem Value="4">Approved absences after 9 o clock</asp:ListItem>
                                <asp:ListItem Value="5">Un-Approved absences aftr 9</asp:ListItem>
                            </asp:RadioButtonList></td>
                        
                        <td valign="top">
                        <div class="Report_criteria_body">
                               <table cellspacing="0" style="color:#FFF" cellpadding="0" width="100%" align="center" border="0">
                                        <tr>
                                            <td class="titlesection" colspan="12" align="left">
                                                Reports</td>
                                        </tr>
                                        <tr>
                    <td align="center" colspan="5" style="width: 100%">
                     <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                <tr id="1">
                                    <td>
                                      
                                                <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                                    <tr id="Tr1" runat="server" >
                                                        <td align="right" style="width: 20%; text-align: left">
                                                        </td>
                                                        <td colspan="4" align="center">
                                                            &nbsp;
                                                            <asp:RadioButton ID="rbMonth" GroupName="a" runat="server" Text="Monthly" Checked="true" AutoPostBack="True" OnCheckedChanged="rbMonth_CheckedChanged" Visible="False" />
                                                            <asp:RadioButton ID="rbRange" GroupName="a" runat="server" Text="Date Ranges" AutoPostBack="True" OnCheckedChanged="rbRange_CheckedChanged" Visible="False" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trMonth" runat="server" >
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            Month:</td>
                                                        <td colspan="4" align="left">
                                                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr style="height: 3px">
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr id="trFrmDate" runat="server" visible = "false">
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            From Date</td>
                                                        <td colspan="4" align="left">
                                                            <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" Width="125px"></asp:TextBox><cc1:CalendarExtender
                                                                ID="CalendarExtender1" runat="server" TargetControlID="txtFrmDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                     <tr style="height: 3px">
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr id="trToDate" runat="server" visible = "false">
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            To Date</td>
                                                        <td colspan="4" align="left">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="30" Width="125px"></asp:TextBox><cc1:CalendarExtender
                                                                ID="CalendarExtender2" runat="server" TargetControlID="txtToDate">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                     <tr style="height: 3px">
                                                        <td colspan="5" align="left">
                                                            <asp:CheckBox ID="chkemp" runat="server" AutoPostBack="True"  
                                                                Text="Single Employee" Visible="False" /></td>
                                                    </tr>
                                                    <tr id="TrDept" runat="server" visible="false">
                                                        <td align="right" style="width: 20%; text-align: left">
                                                            Department:</td>
                                                        <td align="left" colspan="4">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="408px" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="True">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr id="trsingleemp" runat="server" visible = "false">
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            Single Employee:</td>
                                                        <td colspan="4" align="left">
                                                            <asp:DropDownList ID="ddlEmployeecode" runat="server" Width="408px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                     </td>
                                </tr>
                                <tr style="height: 5px">
                                    <td colspan="5">
                                    </td>
                                </tr>
                            </table>
                                                </td>
                </tr>
                                    </table>
                            </div>
                        </td>
                        
                        </tr>
                        
                        
                        </table>


        </div>

            <div class="Reoprts_list_footer">
            	<p class="Show_reoprt_icon">click to Down</p>
            </div>
            

</div>

       </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

