<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KPITemplate_Manage.aspx.cs" Inherits="KPITemplate_Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>
             <div class="container" style="padding: 20px;">
             <h1 style="color:white; text-align:left; font-size:large; font-weight:bold" >Manage KPI Templates</h1>
  <div class="row" style="margin-top: 20px;">
    <div class="col-md-12">
        <div style="display: flex; justify-content: flex-end;">
             <asp:Button ID="btnCreateNew" runat="server" Text="CREATE NEW" CssClass="btn btn-primary" OnClick="btnCreateNew_Click"  />
 <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btn btn-secondary" Style="margin-left: 10px;" OnClick="btnBack_Click" />
            
        </div>
    </div>
</div>
<br />
                 <br />
   
    <asp:GridView ID="gvKPITemplates" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" EmptyDataText="No records found."
           OnRowDeleting="gvKPITemplates_RowDeleting"
    OnRowCommand="gvKPITemplates_RowCommand"
         DataKeyNames="TemplateId"
        >
        <Columns>
            <asp:BoundField DataField="TemplateName_Year" HeaderText="Template Name" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
            <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
            <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd-MMM-yy}" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White"/>
            <asp:BoundField DataField="KPI_Count" HeaderText="# of KPIs" HeaderStyle-ForeColor="Black" HeaderStyle-BackColor="LightGray" HeaderStyle-Font-Bold="true" ItemStyle-ForeColor="White" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"/>

     
     <asp:TemplateField HeaderText="Edit">
    <ItemStyle Width="50px" />
    <HeaderStyle Width="50px" />
    <ItemTemplate>
        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EditTemplate"
            CommandArgument='<%# Eval("TemplateId") %>' CssClass="btn btn-primary" />
    </ItemTemplate>
</asp:TemplateField>

<asp:CommandField ShowDeleteButton="True" ButtonType="Button" 
    HeaderText="Delete" DeleteText="Delete" ControlStyle-CssClass="btn btn-secondary">
    <ItemStyle Width="50px" />
    <HeaderStyle Width="50px" />
</asp:CommandField>
    </Columns>
</asp:GridView>

             </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

