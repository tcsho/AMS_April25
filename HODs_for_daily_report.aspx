<%@ Page Title="HOD's for Daily Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HODs_for_daily_report.aspx.cs" Inherits="HODs_for_daily_report" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="Scripts/jquery.dataTables.min.js">
    </script>
 
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/jquery.timepicker.css" rel="Stylesheet" type="text/css" />
            <script src="Scripts/jquery.timepicker.js" type="text/javascript"></script>
           
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                       HOD's Morning Attendance</h1>
                      
                        <asp:LinkButton ID="AddHOD" OnClick="btn_AddHOD_Click" runat="server" CssClass="btn round_corner" 
                            CausesValidation="False">Add New</asp:LinkButton>
                    </div>
                    <asp:Panel ID="PanelHOD" runat="server"  class="col-lg-10 col-md-10 col-sm-10 col-xs-12 panel" Visible="false">
                       <div class="panel-body">
                             <div class="panel_head">
                                <asp:Label ID="lblAddNew" runat="server" Text="Add New HOD to Morning Attendance"></asp:Label>
                            </div>
                             
                             <div class="form-group">
                             <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                              <asp:label runat="server" ID="lblRegion" for="*Select Region:" class=" col-lg-4 col-md-4 col-sm-4 col-xs-12 control-label" ForeColor="white">*Select Region:</asp:label> 
                            </div>
                             <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                            <asp:DropDownList ID="ddl_region_dept" runat="server" Width="323px" 
                                     AutoPostBack="True" onselectedindexchanged="ddl_region_dept_SelectedIndexChanged">
                            </asp:DropDownList>
                            </div></div>
                               <div class="form-group">
                              <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                              <asp:label runat="server" ID="lblCenter" for="*Select Center:" class=" col-lg-4 col-md-4 col-sm-4 col-xs-12 control-label" ForeColor="white">*Select Center:</asp:label> 
                            </div>
                             <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                                <asp:DropDownList ID="ddl_center_dept" runat="server" Width="323px" 
                                     AutoPostBack="True" 
                                     onselectedindexchanged="ddl_center_dept_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                            </div>
                          
                            
                             <div class="form-group">
                             <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                              <asp:label runat="server" ID="lblDept" for="*Select Department:" class=" col-lg-4 col-md-4 col-sm-4 col-xs-12 control-label" ForeColor="white">*Select Department:</asp:label> 
                              </div>
                              <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                             <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" Width="323px"  onselectedindexchanged="ddl_dept_SelectedIndexChanged" >
                            </asp:DropDownList>
                            </div></div>
                            
                            </div>
                            </asp:Panel>
                            <asp:Panel Class="col-lg-10 col-md-10 col-sm-10 col-xs-12 panel" runat="server" ID="gridView" Visible="false">
                            <div class="panel-body">
                            <div class="panel_head">
                              <asp:Label ID="lblheading" runat="server" Text="List of Employees" ForeColor="white"></asp:Label>
                            </div>
                                <asp:GridView ID="gvEmployees" runat="server"  AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" AllowPaging="True" 
                                AllowSorting="True" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6" BackColor="white"
                                PageSize="50" EmptyDataText="No Data Exist!"  OnPageIndexChanging="gvEmployees_PageIndexChanging" CaptionAlign="Left" 
                                onsorting="gvEmployees_Sorting" onrowcommand="gvEmployees_RowCommand"  >
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                            <asp:TemplateField HeaderText="cb">
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </EditItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="lnkSelect" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBoxHOD" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                               
                        
                        
                    <div class="form-group">
                        
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                        <div class="pull-right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" 
                                Visible="False" onclick="btnSave_Click" />  
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" onclick="btnCancel_Click" />  
                            </div>
                        </div>
                    </div><!--end form group-->
                    </div>
                    </asp:Panel>
  
                   <asp:Panel Class="col-lg-10 col-md-10 col-sm-10 col-xs-12 panel" runat="server" ID="gridMainView"  >
                            <div class="panel-body">
                            <div class="panel_head">
                              <asp:Label ID="lblMainheading" runat="server" Text="List of Employees" ForeColor="white"></asp:Label>
                            </div>
                            
                                    <asp:GridView ID="gvMainHODData" runat="server" Width="100%" AutoGenerateColumns="False"
                                        SkinID="GridView" HorizontalAlign="Center" AllowPaging="True" AllowSorting="True" CssClass="table table-hover" HeaderStyle-BackColor="#c6c6c6"
                                        PageSize="20" EmptyDataText="No Data Exist!"  BackColor="white" OnPageIndexChanging="gvMainHODData_PageIndexChanging"
                                        OnSorting="gvMainHODData_Sorting"  >
                                        <Columns>
                                        <asp:BoundField DataField="HODs_for_daily_report_id" HeaderText="Id" SortExpression="HODs_for_daily_report_id" Visible="false">
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode"   />
                                            <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="CodeName"></asp:BoundField>
                                            <asp:BoundField DataField="Org_code" HeaderText="Organisation Code" SortExpression="Org_code" Visible="false"/>
                                            <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id" Visible="false" />
                                            <asp:BoundField DataField="RegName" HeaderText="Region Name" SortExpression="RegName" Visible="false" ></asp:BoundField>
                                            <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id"  Visible="false"/>
                                             <asp:BoundField DataField="CenName" HeaderText="Center Name" SortExpression="CenName" ></asp:BoundField>
                                              <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode"  Visible="false"/>
                                             <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptName"></asp:BoundField>
                                             <asp:BoundField DataField="DesigCode" HeaderText="Designation" SortExpression="DesigCode"  Visible="false"/>
                                             <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName"></asp:BoundField>
                                              <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("HODs_for_daily_report_id") %>'
                                            CausesValidation="False" CommandName="Delete Details"   OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure to perform this action?')"
                                                ImageUrl="~/images/delete.gif" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                               
                        
                   
                    </div>
                    </asp:Panel>
                </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

