<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" CodeFile="NetworkEmployee.aspx.cs" Inherits="NetworkEmployee" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="css/examples.css" rel="stylesheet" type="text/css" />--%>
    <%-- <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">

//        function toggleSelectionGrid(source) {

//            var isChecked = source.checked;
//           
//            if (source.checked = isChecked) {

//                $("#ctl00_ContentPlaceHolder1_div2 input[id*='CheckBox2']").attr('checked', this.checked=true);
//                alert("This");
//            }
            
            

//            $("#ctl00_ContentPlaceHolder1_div2 input[id*='cbIsHo']").each(function (index) {

//                //                $(this).attr('checked', false);

//            });
        //}



</script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                        Network Employee</h1>
                    <div class="controls round_corner">
                        <div>
                            <asp:LinkButton ID="btnAddNetwork" OnClick="btnAddNetwork_Click" runat="server" CssClass="btn round_corner"
                                CausesValidation="False" Font-Bold="False">Add New [Network Name]</asp:LinkButton>
                                   
                        </div>
                    </div>
                  
                     <asp:Panel ID="Pan_NetworkName" runat="server" class="panel new_event_wraper">
                       
                        <div style="padding: 5px;">
                            <div class="panel_head">
                                <asp:Label ID="lblNetwork" runat="server" Text="Add New Network's Name"></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddlRegions" runat="server" Width="323px" AutoPostBack="True" AppendDataBoundItems="true"  OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                      <asp:TextBox ID="txtNetworkName" Width="320" runat="server" placeholder="Enter Name of Network" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Network Name is Required Field" ControlToValidate="txtNetworkName" ValidationGroup="s"></asp:RequiredFieldValidator>
                            <br />
                           
                  
                      
                            <asp:Button ID="btnSaveNetWorkName"  runat="server" CssClass="button"
                                ValidationGroup="s" Text="Save" onclick="btnSaveNetWorkName_Click"></asp:Button>
                           
                            <asp:Button ID="btnCancel1" OnClick="btnCancel_Click" runat="server" CausesValidation="False"
                                Text="Cancel"></asp:Button>

                                    
                        
                        </div>
                    </asp:Panel>
                     <div class="panel" id="divMainList" runat="server" style="display: inline;">
                        <div class="panel_head">
                            List of Network's
                            <br />
                            <br />
                            <div style="clear: both;">
                            </div>
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                             
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True" Visible="false"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvNetwork" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView"  HorizontalAlign="Center" OnPageIndexChanging="gvNetwork_PageIndexChanging"
                                OnRowDeleting="gvNetwork_RowDeleting" OnSorting="gvNetwork_Sorting" OnRowDataBound="gvNetwork_RowDataBound"
                                AllowPaging="True" AllowSorting="True" PageSize="50" EmptyDataText="No Data Exist!">
                                <Columns>
                                 <asp:BoundField DataField="NetworkRegion_Id" HeaderText="NetworkRegion_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NetworkName" HeaderText="Network Name" SortExpression="NetworkName">
                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                    </asp:BoundField>
                                  
                                   
                                    <asp:BoundField DataField="TotalCampus" HeaderText="Total Campus" SortExpression="TotalCampus" >
                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalTeam" HeaderText="Total Team / Employee's" SortExpression="TotalTeam" >
                                     <ItemStyle HorizontalAlign="Center" Width="300px" />
                                    </asp:BoundField>
                                   
                                    <asp:TemplateField HeaderText="Show Campus">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnShowCampus" runat="server" CommandArgument='<%# Eval("NetworkRegion_Id") %>'
                                                ImageUrl="~/images/details-icon.png" Width="22px" OnClick="btnShowCampus_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add Campus">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAddCampuses" runat="server" CommandArgument='<%# Eval("NetworkRegion_Id") %>'
                                                ImageUrl="~/images/add-icon.png" Width="22px" OnClick="btnAddCampuses_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Show Team">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnShowTeam" runat="server" CommandArgument='<%# Eval("NetworkRegion_Id") %>'
                                                ImageUrl="~/images/details-icon.png" Width="22px" OnClick="btnShowTeam_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Add Team">
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAddTeam" runat="server" CommandArgument='<%# Eval("NetworkRegion_Id") %>'
                                                ImageUrl="~/images/add-icon.png" Width="22px" OnClick="btnAddTeam_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                    </asp:TemplateField>





                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/delete.gif" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="NetworkRegion_Id" HeaderText="NetworkRegion_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                   
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="pan_AssigCampus" runat="server" class="panel new_event_wraper" HorizontalAlign="Left" >
                      
                        <div style="padding: 5px; text-align:left;">
                            <div class="panel_head">
                                <asp:Label ID="lblAddNew" runat="server" Text="Assign Campuses To Network"></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddl_region_dept" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddl_region_dept_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                             <asp:DropDownList ID="ddl_Networks" runat="server" Width="323px" 
                                AppendDataBoundItems="true" 
                               >
                              <asp:ListItem Value="0" Text="-- Select Network --"></asp:ListItem>
                            
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Network"
                             ControlToValidate="ddl_Networks" InitialValue="0"  ValidationGroup="x"  ></asp:RequiredFieldValidator>
                            <br />
                            <asp:DropDownList ID="ddl_center_dept" runat="server" Width="323px" AutoPostBack="True" Visible="false"  
                                OnSelectedIndexChanged="ddl_center_dept_SelectedIndexChanged">
                            </asp:DropDownList>
                              <div class="panel" id="div1" runat="server" style="display: inline;" >
                                <div class="panel_head">
                                    List of Campuses
                                </div>
                                <div class="panel_body" style="text-align:left"  >
                                    <asp:GridView ID="gvAssignCampus" runat="server" Width="100%" AutoGenerateColumns="False"
                                        SkinID="GridView" HorizontalAlign="Left" AllowPaging="True" AllowSorting="True"
                                        PageSize="50" EmptyDataText="No Data Exist!" OnPageIndexChanging="gvAssignCampus_PageIndexChanging"
                                        OnRowCommand="gvAssignCampus_RowCommand"
                                      >
                                        <Columns>
                                        <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id"  >
                                     <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="cb">
                                               
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>

                                            <asp:BoundField  DataField="Center_Name" HeaderText="Campus Name" SortExpression="Center_Name" >
                                             <ItemStyle HorizontalAlign="left" ></ItemStyle>
                                            </asp:BoundField>
                                             
                                     
                                            
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>
                                </div>
                            </div>


                            <br />
                            
                         
                            <asp:Button ID="btnSaveAssignCampus" OnClick="btnSaveAssignCampus_Click" runat="server" CssClass="button"
                                ValidationGroup="x" Text="Save"></asp:Button>
                            
                            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" CausesValidation="False"
                                Text="Cancel"></asp:Button>
                        </div>
                    </asp:Panel>

                   <asp:Panel ID="pan_AssignTeam" runat="server" class="panel new_event_wraper">
                        <%-- <div class="new_event">--%>
                        <div style="padding: 5px;">
                            <div class="panel_head">
                                <asp:Label ID="Label2" runat="server" Text="Assign Network Team Member"></asp:Label>
                            </div>
                            <asp:DropDownList ID="ddl_RegionTeam" runat="server" Width="323px" 
                                AutoPostBack="True" onselectedindexchanged="ddl_RegionTeam_SelectedIndexChanged" 
                               >
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="ddl_TeamNetwork" runat="server" Width="323px" 
                                AppendDataBoundItems="true" 
                               >
                              <asp:ListItem Value="0" Text="-- Select Network --"></asp:ListItem>
                            
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="ddlCenterTeam" runat="server" Width="323px" 
                                AutoPostBack="True" AppendDataBoundItems="true" onselectedindexchanged="ddlCenterTeam_SelectedIndexChanged" Visible="false"
                                >
                            </asp:DropDownList>
                            <br />
                            *Select Department:
                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                            </asp:DropDownList>  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                             ControlToValidate="ddlDepartment" InitialValue="0"  ValidationGroup="y"  ></asp:RequiredFieldValidator>

                             <asp:Button ID="btnSaveNetworkEmployee"  runat="server"
                                CssClass="button" Text="Save"  ValidationGroup="y"
                                onclick="btnSaveNetworkEmployee_Click"></asp:Button>
                            <asp:Button ID="Button1" OnClick="btnCancel_Click" runat="server" CausesValidation="False"
                                Text="Cancel"></asp:Button>
                            <div class="panel" id="div2" runat="server" style="display: inline;">
                                <div class="panel_head">
                                    List of Employees
                                </div>
                                <div class="panel_body">
                                    <asp:GridView ID="gvEmployees" runat="server" Width="100%" AutoGenerateColumns="False"
                                        SkinID="GridView" HorizontalAlign="Center" AllowPaging="True" AllowSorting="True"
                                        PageSize="50" EmptyDataText="No Data Exist!" OnPageIndexChanging="gvEmployees_PageIndexChanging"
                                        OnSorting="gvEmployees_Sorting" OnRowCommand="gvEmployees_RowCommand">
                                        <Columns>
                                          <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode"  >
                                     <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="cb">
                                              
                                                <HeaderTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Is HOD">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbIsHo" runat="server" onclick="toggleSelectionGrid(this);" />
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                            </asp:BoundField>
                                        
                                            <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                        
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                        <SelectedRowStyle CssClass="tr_select" />
                                    </asp:GridView>

                                      
                                </div>
                              
                            </div>
                           
                           
                           
                        </div>
                    </asp:Panel>

                 
                      <div class="panel" id="divListOfCampus" runat="server" style="display: none;">
                        <div class="panel_head">
                            List of Campuses
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvCampuses" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView"  OnPageIndexChanging="gvCampuses_PageIndexChanging"
                                OnRowDeleting="gvCampuses_RowDeleting" OnSorting="gvCampuses_Sorting"
                                OnRowDataBound="gvCampuses_RowDataBound" AllowPaging="True" AllowSorting="True" EmptyDataText="No Data Exist!"
                                PageSize="50">
                                <Columns>
                                    <asp:BoundField DataField="NetworkCenterId" HeaderText="NetworkCenterId">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name">
                                   <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                   
                                    
                                
                                    <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id"  >
                                     <ItemStyle CssClass="hide"   />
                                        <HeaderStyle CssClass="hide" />
                                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NetworkRegion_Id" HeaderText="NetworkRegion_Id" >
                                     <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/delete.gif" Text="" />
                                        </ItemTemplate>
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
                     <div class="panel" id="divListOfTeam" runat="server" style="display: none;">
                        <div class="panel_head">
                            List of Team 
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvTeam" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center" OnPageIndexChanging="gvTeam_PageIndexChanging"
                                OnSorting="gvTeam_Sorting"
                                OnRowDataBound="gvTeam_RowDataBound" AllowPaging="True" AllowSorting="True" EmptyDataText="No Data Exist!"
                                PageSize="50">
                                <Columns>
                                    <asp:BoundField DataField="NetworkTeam_Id" HeaderText="NetworkTeam_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                    
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                     <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                                      <asp:TemplateField HeaderText="Is HOD">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                        
                                          <asp:CheckBox ID="cbHOD" Enabled="false" runat="server" Checked='<%# bool.Parse(Eval("IsHOD").ToString()) %>' Enable='<%# !bool.Parse(Eval("IsHOD").ToString()) %>'/>

                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                      
                                    <asp:TemplateField HeaderText="Remove">
                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelNetworkTeam" runat="server" CommandArgument='<%# Eval("networkTeam_Id") %>'
                                                 ImageUrl="~/images/delete.gif" Text="" Width="22px" OnClick="btnDelNetworkTeam_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="20px" />
                                    </asp:TemplateField>
                                     
                                  
                                    <asp:BoundField DataField="NetworkRegion_Id" HeaderText="NetworkRegion_Id" >
                                     <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                  
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="Label1" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                        </div>
                    </div>
                </div>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

