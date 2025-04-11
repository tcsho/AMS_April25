<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResetHRPassword.aspx.cs" Inherits="ResetHRPassword" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <link href="css/examples.css" rel="stylesheet" type="text/css" />
            <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
            <link href="css/jquery.timepicker.css" rel="Stylesheet" type="text/css" />
            <script src="Scripts/jquery.timepicker.js" type="text/javascript"></script>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="controls round_corner">
                           
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="true"  OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                            </asp:DropDownList>
                          
                            <div style="clear: both;">
                            </div>
                            </div>
                         
                        <div class="panel">
                            <div class="panel_head">
                                List of HR users</div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <!--Paste Grid code here-->
                  <asp:GridView ID="gvShifts" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    EmptyDataText="No record found." HorizontalAlign="Center" OnPageIndexChanging="gvShifts_PageIndexChanging"
                                     OnSorting="gvShifts_Sorting" 
                                    AllowPaging="True" AllowSorting="True" PageSize="200">
                                    <Columns>
                                        <asp:BoundField DataField="user_id" HeaderText="user_id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Region_Id" SortExpression="Region_Id" HeaderText="Region_Id">
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="Center_Id" SortExpression="Center_Id" HeaderText="Center_Id">
                                        </asp:BoundField>

                                        <asp:BoundField DataField="First_Name" SortExpression="First_Name" HeaderText="First_Name">
                                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="User_Name" SortExpression="User_Name" HeaderText="User_Name">
                                           
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmployeeCode" SortExpression="EmployeeCode" HeaderText="EmployeeCode">
                                           
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Password" SortExpression="Password" HeaderText="Password">
                                          <ItemStyle CssClass=""></ItemStyle>
                                            <HeaderStyle CssClass=""></HeaderStyle>
                                        </asp:BoundField>
                                        
                                       
                                        <asp:TemplateField HeaderText="Password Reset">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPassReset" runat="server" CausesValidation="false" CommandArgument='<%# Eval("User_Name") %>'
                                                    ImageUrl="~/images/passrest.png" OnClick="btnPassChange_Click" OnClientClick="return confirm('Are you sure to perform this action?')" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
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