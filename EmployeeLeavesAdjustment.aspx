<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeLeavesAdjustment.aspx.cs" Inherits="EmployeeLeavesAdjustment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <%--<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style type="text/css">
        .controlses {
            margin-top: -30px;
        }

        .LabelWhite {
            font-size: 18px;
            /*color: #FFF;*/
            color: #ffff00;
            margin-bottom: 5px;
            margin-left: 2em;
        }

        .style-changes td{
                vertical-align: top;
            }
            .style-changes textarea{
                resize: vertical;
            }
    </style>
    <script type="text/javascript">
        //function BindEvents() {
        //$(document).ready(function () {

        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            /*Date Picker*/
            $(function () {
                //$(".datepicker").datepicker();
                $('.datepicker').datepicker();
                $("#anim").change(function () {
                    $(".datepicker").datepicker("option", "showAnim", $(this).val());
                });


            });

            //            $('.close').click(function () {
            //                $('.mesg').slideUp("slow");
            //                $('.error_mesg').slideUp("slow");
            //            });



            //             /*********Pnael_expand***********/
            //             $('.panel_head').append('<span class="panel_expand">Plus icon</span>');
            //             $('.panel_expand').click(function () {
            //                 var parentpanel = $(this).parents(".panel");
            //                 $(parentpanel).css("background", "#C60");
            //                 $(parentpanel).find('.panel_body').css("max-height", "100%");
            //                 /*	alert("click");
            //                 $('.panel_body').css("max-height","100%");*/
            //             });


            //             /*********panel_expand***********/



        }

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            document_Ready();
        });
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Employee Leaves Adjustment</h1>


                    <asp:Panel ID="pan_New" runat="server" class="panel">
                        
                        <asp:DropDownList ID="ddlMonths" runat="server" Enabled="true"  OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged" AutoPostBack="True" ><%--AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged"--%>

                        </asp:DropDownList>
                            
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddlEmployeecode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeecode_SelectedIndexChanged">
                        </asp:DropDownList>


                        
                    </asp:Panel>


                    <div class="panel" ID="EmployeeLeaves" runat="server" >
                        <div class="panel_head">
                            Employeewise Leaves
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_EmpLeaves" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gv_EmpLeaves_PageIndexChanging"
                                OnSorting="gv_EmpLeaves_Sorting" AllowPaging="True" EmptyDataText="No Data Exist!"
                                AllowSorting="True" PageSize="50" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6" BackColor="white" >  
                                <Columns>
                                        <asp:BoundField DataField="LeaveId" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                             
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                             <%--           <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee Code"
                                            HtmlEncode="False">
                                          
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="LeaveGroup" HeaderText="Leave Group"  Visible="false">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PMonthDesc" SortExpression="PMonthDesc" HeaderText="Month" Visible="false">
                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveFrom" SortExpression="LeaveFrom" HeaderText="Leave From" DataFormatString="{0:dddd dd/MM/yyyy}">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveTo" SortExpression="LeaveTo" HeaderText="Leave To" DataFormatString="{0:dddd dd/MM/yyyy}">
                                      
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LeaveTypeId" SortExpression="LeaveTypeId" >
                                             <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>

                                    <asp:BoundField DataField="LeaveReason" SortExpression="LeaveReason" >
                                             <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>

                                    <asp:TemplateField HeaderText="Leave Type" SortExpression="LeaveTypeId">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("LeaveTypeId").ToString() == "61" ? "Casual Leave" : Eval("LeaveTypeId").ToString() == "62" ? "Annual Leave" : "Unknown" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton DataField="LeaveId" ID="btnEditLeaves" runat="server" CommandArgument='<%# Eval("LeaveId") %>'
                                                     ImageUrl="~/images/edit.gif" OnClick="btnLeavesEdit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remove">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnReSubmitLeaves" runat="server" CommandArgument='<%# Eval("LeaveId") %>'
                                                    ImageUrl="~/images/delete.gif"   OnClick="btnLeavesResubmit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
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
                     <div class="panel" ID="EmployeeLeaveWithoutPay" runat="server">
                        <div class="panel_head">
                            Employeewise Leave Without Pay
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_LWP" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" 
                                 EmptyDataText="No Data Exist!"
                                 PageSize="50" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6" BackColor="white" >
                                <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="8px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                             
                                        <%--</asp:BoundField>
                                        <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">--%>
                                             
                                        </asp:BoundField>

                                       

                                        <asp:BoundField DataField="SystemRemarks" SortExpression="SystemRemarks" HeaderText="Leave Reason">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                    
                                        <asp:BoundField DataField="LeaveType_Id" SortExpression="LeaveType_Id" >
                                             <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>

                                    <asp:TemplateField HeaderText="Leave Reason" SortExpression="EmpLvReason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("EmpLvReason").ToString() == "" ? Eval("SystemRemarks").ToString(): "Unknown" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="HODLvAprv" SortExpression="HODLvAprv" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                       <%-- <asp:BoundField DataField="HODLvRemarks" SortExpression="HODLvRemarks" HeaderText="HOD Remarks">
                                            
                                        </asp:BoundField>--%>
                                     <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton DataField="Att_Id" ID="btnEditWpLeaves" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                     ImageUrl="~/images/edit.gif" OnClick="btnLWPEdit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remove">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReSubmitHalfDay" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                ImageUrl="~/images/delete.gif"  OnClick="btnReSubmitLWP_Click"  />
                                        </ItemTemplate>
                                        <HeaderStyle  />
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
                    <div class="panel" ID="EmployeeHalfDayLeaves" runat="server">
                        <div class="panel_head">
                            Employeewise Half Day Leaves
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_EmpHalfdayLeaves" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gv_EmpHalfdayLeaves_PageIndexChanging"
                                OnSorting="gv_EmpHalfdayLeaves_Sorting" AllowPaging="True" EmptyDataText="No Data Exist!"
                                AllowSorting="True" PageSize="50" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6" BackColor="white" >
                                <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="8px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                             
                                        </asp:BoundField>

                                      <asp:BoundField DataField="LeaveType_Id" SortExpression="LeaveType_Id" >
                                             <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>

                                        <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Leave Reason">
                                           
                                        </asp:BoundField>
                                       <%-- <asp:BoundField DataField="HODleaveAPV" SortExpression="HODleaveAPV" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODLvRemarks" SortExpression="HODLvRemarks" HeaderText="HOD Remarks">
                                            
                                        </asp:BoundField>--%>
                                       <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton DataField="Att_Id" ID="btnEditHalfDayLeavesLeaves" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                     ImageUrl="~/images/edit.gif" OnClick="btnHalfDayLeavesEdit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remove">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReSubmitHalfDay" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                ImageUrl="~/images/delete.gif"  OnClick="btnReSubmitHalfDay_Click"  />
                                        </ItemTemplate>
                                        <HeaderStyle  />
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
                    <div class="panel" ID="EmployeeMissing" runat="server">
                        <div class="panel_head">
                            Employeewise Missing In/Out Leaves
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_EmpMissing" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-hover" 
                            HeaderStyle-BackColor="#c6c6c6" BackColor="white" SkinID="GridView" HorizontalAlign="Center" 
                            OnPageIndexChanging="gv_EmpMissing_PageIndexChanging" OnSorting="gv_EmpMissing_Sorting"  AllowPaging="True"
                            AllowSorting="True" PageSize="50" EmptyDataText="No Data Exist!">
                                  <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOEmpReason" SortExpression="MIOEmpReason" HeaderText="Employee Reason">
                                            
                                        </asp:BoundField>

                                      <asp:BoundField DataField="AttendanceTypeId" SortExpression="AttendanceTypeId" >
                                             <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                       <%-- <asp:BoundField DataField="MIOHODAprv" SortExpression="MIOHODAprv" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOApprovalReason" SortExpression="MIOApprovalReason"
                                            HeaderText="HOD Remarks">
                                           
                                        </asp:BoundField>--%>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton DataField="Att_Id" ID="btnEditMissingLeaves" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                     ImageUrl="~/images/edit.gif" OnClick="btnMissingLeavesEdit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                      <%--<asp:TemplateField HeaderText="Remove">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReSubmitMissing" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                ImageUrl="~/images/delete.gif"  OnClick="btnReSubmitMissingInOut_Click"  />
                                        </ItemTemplate>
                                        <HeaderStyle  />
                                    </asp:TemplateField>--%>
                                    </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label3" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>
                    <%--<div class="panel" ID="EmployeeLate" runat="server">
                        <div class="panel_head">
                            Employeewise Late
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gv_EmpLate" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table table-hover" 
                            HeaderStyle-BackColor="#c6c6c6" BackColor="white" SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gv_EmpLate_PageIndexChanging"
                                OnSorting="gv_EmpLate_Sorting" AllowPaging="True" AllowSorting="True" PageSize="50" EmptyDataText="No Data Exist!">
                                 <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="8px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegEmpReason" SortExpression="NegEmpReason" HeaderText="Employee Reason">
                                            
                                        </asp:BoundField>
                                     
                                       <asp:TemplateField HeaderText="Edit">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton DataField="Att_Id" ID="btnEditEmpLate" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                     ImageUrl="~/images/edit.gif" OnClick="btnEmpLateEdit_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Remove">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnReSubmitHalfDay" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                ImageUrl="~/images/delete.gif"  OnClick="btnReSubmitLateArrivals_Click"  />
                                        </ItemTemplate>
                                        <HeaderStyle  />
                                    </asp:TemplateField>
                                    </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label4" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>--%>
                    
                    <asp:Panel ID="employeeLvSubmitionDiv" runat="server" >
                        <div class="panel" style="display: flex;">
                            <div style="margin: 0 auto;">
                               
                                <table style="width: 700px;" cellspacing="0" cellpadding="0" align="center">
                                    <tr id="lvdata" runat="server">
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td>
                                            <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                     <%--<td style="width: 20%">Total leaves for year 2023 : 45 </td>--%>

                                                  <%--  <td align="left">
                                                        <asp:Label ID="totalLeavesPerYear" Text="45" runat="server" Width="140px" 
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>--%>
                                                </tr>
                                                <%--<tr>
                                                     <td style="width: 20%">Availed : </td>

                                                    <td align="left">
                                                        <asp:Label ID="availedLeaves" Visible="false" runat="server" Width="140px" 
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                     <td style="width: 20%">Balance : </td>

                                                    <td align="left">
                                                        <asp:Label ID="balanceLeaves" Visible="false" runat="server" Width="140px" 
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="width: 20%">From Date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="140px" CssClass="datepicker" Enabled="false"
                                                            AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>&nbsp;
                                                        (MM/DD/YYYY)
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                            Display="Dynamic" ErrorMessage="*Select From Date" InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">To Date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="140px" CssClass="datepicker" AutoPostBack="True" Enabled="false"
                                                            OnTextChanged="txtToDate_TextChanged"></asp:TextBox>&nbsp;(MM/DD/YYYY)
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                                            Display="Dynamic" ErrorMessage="*Select To Date " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">No. of Days:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtDays" runat="server" Width="50px" MaxLength="100" CssClass="textbox" Enabled="false"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">Reservation Type*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlLeaveType" runat="server" Width="142px" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="dropdownlist">
                                                             <asp:ListItem Text="Select Leave Type" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlLeaveType"
                                                            Display="Dynamic" ErrorMessage="*Select Reservation Type " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                 <tr>
                                                    <td style="width: 20%; vertical-align: top;" valign="top">Employee Remarks:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="empRemarks" runat="server" Width="95%" Height="50px" MaxLength="500"
                                                            Rows="15" TextMode="MultiLine" CssClass="textbox" Enabled="false"></asp:TextBox>
                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: top;" valign="top">HR Remarks:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtReason" runat="server" Width="95%" Height="50px" MaxLength="500"
                                                            Rows="15" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReason"
                                                            Display="Dynamic" ErrorMessage="*Reservation Reason Required !" InitialValue="0"
                                                            ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <asp:TextBox ID="empLeaveId" runat="server"
                                                            Rows="15" CssClass="textbox" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="attId" runat="server"
                                                            Rows="15" CssClass="textbox" Visible="false"></asp:TextBox>

                                                  <asp:TextBox ID="typeGrid" runat="server"
                                                            Rows="15" CssClass="textbox" Visible="false"></asp:TextBox>

                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 10px">
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr id="lvbtn" runat="server">
                                        <td style="width: 5%"></td>
                                        <td align="center">
                                            <asp:Label ID="lbl_res_error" runat="server" Text=""></asp:Label>
                                            <br />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="S"
                                                CssClass="button" />
                                            &nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Panel ID="laveBalanceDiv" runat="server" >
                                <div style="margin: 0 auto; display: ruby;">
                                     <div style="color: white; font-size: large; font-family: cursive;">Leave balance</div>
                                     <table style="width: 600px;" cellspacing="0" cellpadding="0">
                                        <tr id="Tr1" runat="server">
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <table style="width: 100%; margin-left: 190px" cellspacing="0" cellpadding="0">
                                                    <tr style="height: 15px">
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                         <td style="width: 32%; font-weight: 600; color: white;">Available Casual Leaves :  </td>

                                                        <td align="left" style="color: white; font-weight: 600;">
                                                            <asp:Label ID="casualLeaves" runat="server" Width="140px" 
                                                                AutoPostBack="True"></asp:Label>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 24px;">
                                                         <td style="width: 32%; font-weight: 600; color: white;">Available Annual Leaves : </td>

                                                        <td align="left" style="color: white; font-weight: 600;">
                                                            <asp:Label ID="annualLeaves" runat="server" Width="140px"
                                                                AutoPostBack="True"></asp:Label>&nbsp;
                                                        </td>
                                                    </tr>
                                                

                                                    <tr style="height: 3px">
                                                        <td colspan="2"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                   
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

