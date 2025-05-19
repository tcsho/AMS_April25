<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KPITemplate_Assign.aspx.cs" Inherits="KPITemplate_Assign" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Bootstrap Bundle (includes Popper) -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>

<!-- Multiselect CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap-multiselect@1.1.0/dist/css/bootstrap-multiselect.css" rel="stylesheet" />

<!-- Multiselect JS (Only once!) -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap-multiselect@1.1.0/dist/js/bootstrap-multiselect.min.js"></script>

    <style>
         .lable {
     color:white;
     font-size:16px;
     font-weight:bold;
 }
          .multiselect-container>li>a>label {
        padding: 4px 20px 4px 38px; /* spacing for checkbox */
        white-space: nowrap;
        display: block;
        font-weight: normal;
        line-height: 1.5;
    }

    .multiselect-container>li>a>label>input[type=checkbox] {
        margin-left: -20px; /* shift checkbox to left */
        margin-right: 5px;
        position: absolute;
        top: 5px;
        left: 12px;
    }

    .multiselect-container {
        max-height: 300px;
        overflow-y: auto;
    }

    .multiselect {
        text-align: left !important;
    }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <div class="container" style="padding: 20px;">
             <h1 style="color:white; text-align:center; font-size:large; font-weight:bold" >Assign KPI Template</h1>
  <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <div style="display: flex; justify-content: flex-end;">
            <asp:Button ID="btnApply" runat="server" Text="APPLY" CssClass="btn btn-primary" Style="margin-left: 10px;" OnClick="btnApply_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="BACK" CssClass="btn btn-secondary" Style="margin-left: 10px;" OnClick="btnCancel_Click"/>
        </div>
    </div>
</div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-3">
                    <label class="lable">Template Name </label><br />
                    <asp:DropDownList ID="ddlTemplate" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="lable">Year </label><br />
                   <asp:Label ID="lblYear" runat="server" CssClass="lable" Text="-"></asp:Label>
                </div>
                <div class="col-md-2">
                    <label class="lable">From Date </label><br />
                   <asp:Label ID="lblFromDate" runat="server" CssClass="lable" Text="-"></asp:Label>
                </div>
                <div class="col-md-2">
                    <label class="lable">To Date </label><br />
                    <asp:Label ID="lblToDate" runat="server" CssClass="lable" Text="-"></asp:Label>
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-3">
    <label class="lable">Employee Category </label>
    <asp:DropDownList ID="ddlEmpCat" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmpCat_SelectedIndexChanged" AutoPostBack="true">
              <asp:ListItem Text="Select" Value="0" />
              <asp:ListItem Text="SLT" Value="SLT" />
              <asp:ListItem Text="Admin" Value="Admin" />
              <asp:ListItem Text="NP" Value="NP" />
              <asp:ListItem Text="SG" Value="SG" />
              <asp:ListItem Text="Others" Value="Others" />
          </asp:DropDownList>
</div>
                                <div class="col-md-2">
                    <asp:Label ID="lblEmp" runat="server" CssClass ="lable" Text="Employee ID" Visible="false"></asp:Label><br />
   
  <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" Visible="false" OnTextChanged="txtEmpId_TextChanged" AutoPostBack="true" onkeypress="checkEnter(event)"></asp:TextBox>
</div>

                 <div class="col-md-2">
                     
      <asp:Label ID="lblRegion" runat="server" CssClass ="lable" Text="Region"></asp:Label><br />
   <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true" Width="200px">
       <asp:ListItem Text="Select" Value="0" />
          <asp:ListItem Text="(TCS)-Southern Region" Value="20000000" />
          <asp:ListItem Text="(TCS) Northern Region" Value="30000000" />
          <asp:ListItem Text="(TCS) Central Region" Value="40000000" />
      </asp:DropDownList>
 </div>
                </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12 text-center">
                    <h5><b class="lable">APPLY TO:</b></h5>
                </div>
            </div>
            <br />
           
              <asp:GridView ID="gvEmployeeList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" EmptyDataText="No records found." DataKeyNames="EmployeeCode" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvEmployeeList_PageIndexChanging" OnRowDataBound="gvEmployeeList_RowDataBound">
     <PagerStyle BackColor="#eeeeee" ForeColor="Black" HorizontalAlign="Center" />
                  <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox ID="chkSelectAll" runat="server" OnClick="toggleSelectAll(this)" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("TemplateAssigned").ToString().ToLower().StartsWith("yes") %>' />
            </ItemTemplate>
            <HeaderStyle BackColor="LightGray" />
            <ItemStyle ForeColor="White" />
        </asp:TemplateField>
        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee ID" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
        <asp:BoundField DataField="FullName" HeaderText="Name" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
        <asp:TemplateField HeaderText="Assign Center" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" Visible="false">
    <ItemTemplate>
        <asp:ListBox ID="lstKPI" runat="server" SelectionMode="Multiple" CssClass="multi-select">
        </asp:ListBox>
    </ItemTemplate>
</asp:TemplateField>
                      <asp:TemplateField HeaderText="Assign Key Stage" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true">
            <ItemTemplate>
                <asp:ListBox ID="lstKPI1" runat="server" SelectionMode="Multiple" CssClass="multi-select">
                </asp:ListBox>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Assign Class" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true">
            <ItemTemplate>
                <asp:ListBox ID="lstKPI2" runat="server" SelectionMode="Multiple" CssClass="multi-select">
                </asp:ListBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DesigName" HeaderText="Designation" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
        <asp:BoundField DataField="Center_Name" HeaderText="Organization" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
        <asp:BoundField DataField="TemplateAssigned" HeaderText="Template Assigned" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
    </Columns>
</asp:GridView>
            <script type="text/javascript">
    function toggleSelectAll(source) {
        var checkboxes = document.querySelectorAll('[id$=gvEmployeeList] input[type=checkbox]');
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i] != source)
                checkboxes[i].checked = source.checked;
        }
                }
                function enableMultiselect() {
                    $('.multi-select').each(function () {
                        $(this).multiselect({
                            includeSelectAllOption: true,
                            buttonWidth: '180px',
                            nonSelectedText: 'Select options',
                            allSelectedText: 'All selected'
                        });
                    });
                }

                // Call this when page loads
                Sys.Application.add_load(enableMultiselect);
                function checkEnter(event) {
                    if (event.key === "Enter") {
                        event.preventDefault(); // Default form submit rokne ke liye
                        yourFunction(); // Apna function call karain
                    }
                }
            </script>
            <div class="row" style="margin-top: 20px;">
    <div class="col-md-12 text-center">
        
    </div>
</div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

