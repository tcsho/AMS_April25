<%@ Page Title="User Password Reset" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeePasswordReset.aspx.cs" Inherits="EmployeePasswordReset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Reset Employee Password</h1>
                    <div class="controls round_corner">
                        <div>
                        </div>
                    </div>
                    <div style="padding: 2px;">
                        <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" Enabled="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" Enabled="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlDept" runat="server" Width="323px" Enabled="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <%--------------------------------------------------------Gridview Panel--------------------------------------------------------------------%>
                    <asp:Panel ID="pEmployee" runat="server" class="panel">
                        <div class="panel_head">
                            <asp:Label ID="lblheading" runat="server" Text="List of Employees "></asp:Label>
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" SkinID="GridView"
                                HorizontalAlign="Center" AllowPaging="True" AllowSorting="True" CssClass="table table-hover"
                                HeaderStyle-BackColor="#c6c6c6" BackColor="white" PageSize="50" EmptyDataText="No Data Exist!"
                                OnPageIndexChanging="gvEmployees_PageIndexChanging" OnSorting="gvEmployees_Sorting">
                                <Columns>
                                    <asp:BoundField DataField="User_Id" HeaderText="User_Id" SortExpression="User_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="RegName" HeaderText="Region Name" SortExpression="RegName" />
                                    <asp:BoundField DataField="CenName" HeaderText="Center Name" SortExpression="CenName" />
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptName" />
                                    <asp:BoundField DataField="DesigName" HeaderText="Designation Name" SortExpression="DesigName" />
                                    <asp:BoundField DataField="User_Name" HeaderText="User Name" SortExpression="User_Name" />
                                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                                    <asp:TemplateField ShowHeader="True" HeaderText="Reset Password">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnUpdatePassword" runat="server" CommandArgument='<%# Eval("User_Name") %>'
                                                CausesValidation="False" CommandName="Update Password" OnClick="btnUpdatePassword_Click"
                                                ImageUrl="~/images/passrest.png" Text="" />
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
                    <%--------------------------------------------------------New Entry Panel--------------------------------------------------------------------%>
                    <asp:Panel ID="pUpdatepassword" runat="server" class="panel" Visible="false">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 form-group">
                            <div class="panel_head">
                                <asp:Label ID="blheading2" runat="server" Text=" Update Password "></asp:Label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                                <asp:Label runat="server" ID="lblUser" for="UserName: " ForeColor="white" class="control-label"
                                    Text="*User Name :"></asp:Label>
                                <asp:Label runat="server" ID="lblUN" ForeColor="white" Text="" class="control-label"></asp:Label>
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                                <asp:TextBox ID="txtPassword" runat="server" placeholder="New Password" CausesValidation="true">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="Password" runat="server" ControlToValidate="txtPassword" ForeColor="White"
                                    ErrorMessage="Password is required!" SetFocusOnError="True" Display="Dynamic" ValidationGroup="c"  />
                            </div>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                                <asp:TextBox ID="txtConfirm" runat="server" placeholder="Confirm Password" CausesValidation="true">
                                </asp:TextBox>
                                <asp:CompareValidator runat="server" ID="Comp1" ControlToValidate="txtConfirm" ControlToCompare="txtPassword"
                                    Text="Password Mismatch" ForeColor="White" CssClass="control-label" />
                            </div>
                            <div class="col-lg-12 text-right">
                                <div class="pull-right">
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
