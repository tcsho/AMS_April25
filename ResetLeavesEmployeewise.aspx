<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResetLeavesEmployeewise.aspx.cs" Inherits="ResetLeavesEmployeewise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Employeewise Leaves Reset</h1>


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
                                        <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="Employee Code"
                                            HtmlEncode="False">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveGroup" HeaderText="Leave Group"  Visible="false">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PMonthDesc" SortExpression="PMonthDesc" HeaderText="Month" Visible="false">
                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveFrom" SortExpression="LeaveFrom" HeaderText="Leave From" DataFormatString="{0:MM/dd/yyyy}">
                                          
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveTo" SortExpression="LeaveTo" HeaderText="Leave To" DataFormatString="{0:MM/dd/yyyy}">
                                      
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="">
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
                                        <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                             
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Leave Reason">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODleaveAPV" SortExpression="HODleaveAPV" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODLvRemarks" SortExpression="HODLvRemarks" HeaderText="HOD Remarks">
                                            
                                        </asp:BoundField>
                                      <asp:TemplateField HeaderText="">
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
                            <asp:Label ID="Label1" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
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
                                        <asp:BoundField DataField="MIOHODAprv" SortExpression="MIOHODAprv" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOApprovalReason" SortExpression="MIOApprovalReason"
                                            HeaderText="HOD Remarks">
                                           
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="">
                                            <ItemStyle HorizontalAlign="Center"/>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnReSubmitMissing" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                    ImageUrl="~/images/delete.gif"  OnClick="btnReSubmitMissingInOut_Click"  />
                                            </ItemTemplate>
                                            <HeaderStyle/>
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
                    <div class="panel" ID="EmployeeLate" runat="server">
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
                                        <asp:BoundField DataField="ApprovalReason" SortExpression="ApprovalReason" HeaderText="HOD Apv">
                                            
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApprovalReason" SortExpression="ApprovalReason" HeaderText="HOD Remarks">
                                           
                                        </asp:BoundField>
                                       <asp:TemplateField HeaderText="">
                                                    <ItemStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReSubmitLateArrivals" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                            ImageUrl="~/images/delete.gif"   OnClick="btnReSubmitLateArrivals_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle />
                                                </asp:TemplateField>
                                    </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label3" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

