<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KPITemplate.aspx.cs" Inherits="KPITemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
         .lable {
     color:white;
     font-size:16px;
     font-weight:bold;
 }
    </style>
   <script type="text/javascript">

       $(document).ready(document_Ready);

       function document_Ready() {
           $('.show_hide_btn').click(function () {
               $('.approved').slideToggle()
           });
           $('.leave_balance .footer').click(function () {
               $('.leave_balance .header').slideToggle("slow");
               $('.leave_icon').toggleClass('leave_icon_hide');
           });


           /*Date Picker*/
           $(function () {
               $(".datepicker").datepicker();
               $("#anim").change(function () {
                   $(".datepicker").datepicker("option", "showAnim", $(this).val());
               });
           });
       }

       //Re-bind for callbacks
       var prm = Sys.WebForms.PageRequestManager.getInstance();

       prm.add_endRequest(function () {
           document_Ready();
       });

</script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <div class="container" style="padding: 20px;">
       <asp:Label ID="lblHeading" runat="server" 
    Style="color:white; text-align:center; font-size:large; font-weight:bold; display:block;">
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
                <div class="col-md-3">
                    <label class="lable">Template Name <span style="color:red;">*</span></label><br />
                    <asp:TextBox ID="txtTemplateName" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-2">
                    <label class="lable">Year <span style="color:red;">*</span></label>
                   <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                       <asp:ListItem Text="Select" Value="0" />
                                <asp:ListItem Text="2025" Value="2025" />
                                <asp:ListItem Text="2026" Value="2026" />
                                <asp:ListItem Text="2027" Value="2027" />
                                <asp:ListItem Text="2028" Value="2028" />
                                <asp:ListItem Text="2029" Value="2029" />
                                <asp:ListItem Text="2030" Value="2030" />
                            </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <label class="lable">From Date <span style="color:red;">*</span></label>
                    <asp:TextBox ID="txtFromDate" runat="server" Width="140px" CssClass="datepicker form-control"
    AutoPostBack="True"></asp:TextBox>&nbsp;(DD/MM/YYYY)
                </div>
                <div class="col-md-2">
                    <label class="lable">To Date <span style="color:red;">*</span></label>
                     <asp:TextBox ID="txtToDate" runat="server" Width="140px" CssClass="datepicker form-control" AutoPostBack="True" OnTextChanged="txtToDate_TextChanged" ></asp:TextBox>&nbsp;&nbsp;(DD/MM/YYYY)
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-3">
    <label class="lable">Total Weightage of KPIs (%) <span style="color:red;">*</span></label>
    <asp:TextBox ID="txtTotalWeight" runat="server" CssClass="form-control" />
</div>
                </div>

            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12 text-center">
                    <h5><b class="lable">KPI List</b></h5>
                </div>
            </div>

           
              <asp:GridView ID="gvKPIInsert" runat="server" AutoGenerateColumns="False" EnableViewState="true"
    ShowHeader="true" CssClass="table table-bordered mt-3" OnRowCommand="gvKPIInsert_RowCommand" OnRowCreated="gvKPIInsert_RowCreated" OnRowDataBound="gvKPIInsert_RowDataBound" >
    <Columns>
        <asp:TemplateField HeaderText="KPI Name" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtKPIName" runat="server" CssClass="form-control" Width="160px" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Weight(%)" HeaderStyle-ForeColor="White">
    <ItemTemplate>
        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" Width="80px" />
    </ItemTemplate>
</asp:TemplateField>
        <%-- Example for G5 --%>
        <asp:TemplateField HeaderText="G5 Max" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG5Max" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G5 Min" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG5Min" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="G4 Max" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG4Max" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G4 Min" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG4Min" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="G3 Max" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG3Max" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G3 Min" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG3Min" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="G2 Max" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG2Max" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G2 Min" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG2Min" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderText="G1 Max" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG1Max" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="G1 Min" HeaderStyle-ForeColor="White">
            <ItemTemplate>
                <asp:TextBox ID="txtG1Min" runat="server" CssClass="form-control" />
            </ItemTemplate>
        </asp:TemplateField>

     <asp:TemplateField>
    <ItemTemplate>
        <div style="display: flex; gap: 8px; justify-content: center;">
            <asp:Button ID="btnAdd" runat="server" Text="✔" CssClass="btn btn-info" CommandName="InsertRow" />
            <asp:Button ID="btnDelete" runat="server" Text="✖" CssClass="btn btn-danger" CommandName="DeleteRow" />
        </div>
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>


        </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

