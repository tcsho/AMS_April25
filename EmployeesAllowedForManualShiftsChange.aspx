<%@ Page Title="Employees Allowed For Manual Shifts Change" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeesAllowedForManualShiftsChange.aspx.cs" Inherits="EmployeesAllowedForManualShiftsChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">
                         Employee Manual Shift Plan</h1>
                    <div class="controls round_corner">
                        <div>
                         
                            <asp:LinkButton ID="btnAddHOD" OnClick="btnAddHOD_Click" runat="server" CssClass="btn round_corner"
                                CausesValidation="False" Font-Bold="False">Add New </asp:LinkButton>
                               <div runat="server" id="ShiftDetail" class="panel_head"> 
                              <table runat="server" id="EmployeeShiftDetail" >
                             
                              <tr>
                            <td><asp:DropDownList ID="ddlMonths" runat="server" AutoPostBack="True"  CssClass="hidden" >
                            </asp:DropDownList> </td></tr>
                             <tr>
                                 <td>
                                     *Select Department: &nbsp;</td>
                                 <td width="50%">
                                     <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" 
                                         onselectedindexchanged="ddlDepartment_SelectedIndexChanged" Height="30px" 
                                         Width="400px">
                                     </asp:DropDownList>
                                 </td></tr>
                                  <tr>
                                      <td>
                                          *Select Employee: &nbsp;</td>
                                      <td width="50%">
                                          <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" 
                                              Width="400px" Height="30px">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          *Reason:</td>
                                      <td width="50%">
                                          <asp:TextBox ID="txtreason" runat="server"  CssClass="form-control"
                                          TextMode="MultiLine" MaxLength="500" Width="400px"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr><td></td><td>
                                          <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn round_corner" 
                                              OnClick="btnCancel_Click" Text="Cancel" Width="165px" />
                                          &nbsp;<asp:Button ID="btnSaveHOD" runat="server" CssClass="btn round_corner" 
                                              OnClick="btnSaveHOD_Click" Text="Save" ValidationGroup="s" Width="165px" />
                                      </td>
                                  </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    
                    <div runat="server" class="panel" id="divListOfSubordinates"   >
                        <div class="panel_head">
                            List of Employees with Manual Shift Plan
                        </div>
                        <div class="panel_body">
                            <asp:GridView ID="gvSubordinates" runat="server" Width="100%" AutoGenerateColumns="False"
                                SkinID="GridView" HorizontalAlign="Center"  
                                 OnPageIndexChanging="gvSubordinates_PageIndexChanging"
                                OnRowDeleting="gvSubordinates_RowDeleting" OnSorting="gvSubordinates_Sorting"
                                OnRowDataBound="gvSubordinates_RowDataBound" AllowPaging="True" AllowSorting="True"
                                PageSize="15" EmptyDataText="No Data Exist!"> 
                                <Columns>
                                    <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CodeName" HeaderText="Employee"  SortExpression="EmployeeCode"/>
                                    <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    
                                     
                                    <asp:BoundField DataField="RegName" HeaderText="Region Name" SortExpression="Region_Id">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CenName" HeaderText="Center Name" SortExpression="Center_Id" />
                                      <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="DesigCode" HeaderText="DesigCode" SortExpression="DesigCode">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptCode" />
                                    <asp:BoundField DataField="DesigName" HeaderText=" Designation" SortExpression="DesigCode" />
                                    
                                    <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                            CausesValidation="False" CommandName="Delete"
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

