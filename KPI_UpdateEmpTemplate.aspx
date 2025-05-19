<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KPI_UpdateEmpTemplate.aspx.cs" Inherits="KPI_UpdateEmpTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />--%>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
<style>
    .custom-gridview th {
        background-color: #343a40;
        color: white;
        font-weight: bold;
        text-align: center;
        vertical-align: middle;
        padding: 8px;
    }

    .custom-gridview td {
        background-color: #34495e;
        color: white !important; /* force white text */
        vertical-align: middle;
        padding: 8px;
        height: 40px;
    }

    .custom-gridview {
        border-collapse: collapse;
        border-radius: 10px;
        overflow: hidden;
        width: 100%;
        table-layout: fixed;
    }
</style>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <div class="container" style="padding: 20px;">
       <asp:Label ID="lblHeading" runat="server" 
    Style="color:white; text-align:center; font-size:large; font-weight:bold; display:block;">
           Update Employee KPIs
</asp:Label>

  <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <div style="display: flex; justify-content: flex-end;">
            <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-secondary" Style="margin-left: 10px;" OnClick="btnCancel_Click" />
           
        </div>
    </div>
</div>
  <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <div style="display: flex; justify-content: flex-end;">
           <asp:Label ID="lblStatus" runat="server" Visible="false" ForeColor="White" Text ="Data Updated Successfully" Font-Size="Large" ></asp:Label>
        </div>
    </div>
</div>
           
 
        <div class="row" style="margin-top: 20px;">
    <!-- Employee ID -->
  <div class="col-md-2">
    <asp:Label ID="lblEmpID" runat="server" Visible="true" ForeColor="White" Text ="Employee ID"></asp:Label>
    <div class="input-group">
        <asp:TextBox ID="txtEmpID" runat="server" CssClass="form-control" OnTextChanged="txtEmpID_TextChanged" AutoPostBack="true" onkeypress="checkEnter(event)"/>
    </div>
</div>
    <!-- Name -->
    <div class="col-md-2">
        <asp:Label ID="lblEmp" runat="server" Visible="true" ForeColor="White" Text ="Employee Name"></asp:Label>
<div class="input-group">
    <asp:Label ID="lblEmpName" runat="server" Visible="false" ForeColor="White" Text="-"></asp:Label>
</div>
    </div>

    <!-- Organization -->
        <div class="col-md-2">
        <asp:Label ID="lblOrg" runat="server" Visible="true" ForeColor="White" Text ="Organization"></asp:Label>
<div class="input-group">
    <asp:Label ID="lblOrganization" runat="server" Visible="false" ForeColor="White" Text="-"></asp:Label>
</div>
    </div>

    <!-- Designation -->
            <div class="col-md-2">
        <asp:Label ID="lblDes" runat="server" Visible="true" ForeColor="White" Text ="Designation"></asp:Label>
<div class="input-group">
    <asp:Label ID="lblDesignation" runat="server" Visible="false" ForeColor="White" Text="-"></asp:Label>
</div>
    </div>

    <!-- Region -->
            <div class="col-md-2">
        <asp:Label ID="lblReg" runat="server" Visible="true" ForeColor="White" Text ="Region"></asp:Label>
<div class="input-group">
    <asp:Label ID="lblRegion" runat="server" Visible="false" ForeColor="White" Text="-"></asp:Label>
</div>
    </div>
</div>

            <div class="row" style="margin-top: 15px;">
    <div class="col-md-12">
               <asp:Label ID="lblTempName" runat="server" Visible="true" ForeColor="White" Text ="KPI Template assigned :"></asp:Label>

&nbsp;    <asp:Label ID="lblTemplate" runat="server" Visible="true" ForeColor="White" Text="-"></asp:Label>

    </div>
</div>
            
<br />
            <br />
    <asp:GridView ID="gvKPIInsert" runat="server" AutoGenerateColumns="False"
    CssClass="table table-bordered text-center custom-gridview"
    OnRowCreated="gvKPIInsert_RowCreated" DataKeyNames="TemplateDetailID,AssignDetailID">
    <Columns>
        <asp:BoundField HeaderText="Id" DataField="TemplateDetailID" ReadOnly="true" Visible="false"/>
        <asp:BoundField HeaderText="AssignId" DataField="AssignDetailID" ReadOnly="true" Visible="false"/>
        <asp:BoundField HeaderText="KPI Name" DataField="KPIName" ReadOnly="true" />
        <asp:BoundField HeaderText="Weight" DataField="Weight" ReadOnly="true" />

        
        <asp:TemplateField HeaderText="Max">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade5Max" runat="server" Text='<%# Eval("Grade5_Max") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Min">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade5Min" runat="server" Text='<%# Eval("Grade5_Min") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="Max">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade4Max" runat="server" Text='<%# Eval("Grade4_Max") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Min">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade4Min" runat="server" Text='<%# Eval("Grade4_Min") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

        
        <asp:TemplateField HeaderText="Max">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade3Max" runat="server" Text='<%# Eval("Grade3_Max") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Min">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade3Min" runat="server" Text='<%# Eval("Grade3_Min") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="Max">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade2Max" runat="server" Text='<%# Eval("Grade2_Max") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Min">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade2Min" runat="server" Text='<%# Eval("Grade2_Min") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

       
        <asp:TemplateField HeaderText="Max">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade1Max" runat="server" Text='<%# Eval("Grade1_Max") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Min">
            <ItemTemplate>
                <asp:TextBox ID="txtGrade1Min" runat="server" Text='<%# Eval("Grade1_Min") %>' CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

            <br />
              <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <div style="display: flex; justify-content: flex-end;">
            <asp:Label ID="lblUpdated" runat="server" Visible="false" ForeColor="White" Text ="Last Updated:"></asp:Label> &nbsp;&nbsp;
            <asp:Label ID="lblUpdatedOn" runat="server" Visible="false" ForeColor="White" Text =""></asp:Label> &nbsp;
            <asp:Label ID="lblBy" runat="server" Visible="false" ForeColor="White" Text =""></asp:Label> &nbsp;
            <asp:Label ID="lblUpdatedBy" runat="server" Visible="false" ForeColor="White" Text =""></asp:Label>
        </div>
    </div>
</div>
            <br />
              <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <asp:Label ID="lblRemarks" runat="server" Visible="false" ForeColor="White" Text ="Remarks (at least 50 characters required)"></asp:Label>
<div class="input-group">
    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Placeholder ="Type here" TextMode="MultiLine" Width="1100px" Height="60px" Visible="false"/>
</div>
    </div>
</div>
            </div>
            <script type="text/javascript">
                function checkEnter(event) {
                    if (event.key === "Enter") {
                        event.preventDefault(); // Default form submit rokne ke liye
                        yourFunction(); // Apna function call karain
                    }
                }
            </script>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

