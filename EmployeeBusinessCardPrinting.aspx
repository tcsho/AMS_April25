﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeBusinessCardPrinting.aspx.cs" Inherits="EmployeeBusinessCardPrinting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="outer_wrap">
        <div class="fullrow">
            <div class="inner_wrap">
                <p class="page_heading">
                    Business Card for Printing</p>
                     <div class="panel">
                    <div class="panel_head">
                        List Of Approved Requests For Printing
                    </div>
                    <div class="panel_body" style="max-height: 100%; overflow: auto;">
                        <asp:GridView ID="gv" runat="server"   
                        Width="100%" AutoGenerateColumns="False" SkinID="GridView"
                            HorizontalAlign="Center" OnPageIndexChanging="gv_PageIndexChanging"  
                            OnSorting="gv_Sorting"   OnRowCommand="gv_RowCommand" 
                            AllowPaging="True" AllowSorting="True"
                            PageSize="50" EmptyDataText="No Data Exist!">
                            <Columns>
                             <asp:BoundField DataField="EmpBCard_Id" HeaderText="Id" SortExpression="EmpBCard_Id"   >
                              <ItemStyle CssClass="hide" />
                                    <HeaderStyle CssClass="hide" />
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Region_Name" HeaderText="Region Name" SortExpression="Region_Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name">
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                </asp:BoundField>
                                <asp:TemplateField  HeaderText="Sample Card" Visible="false">
                                            <ItemTemplate>
                                                <div style="width: 100%;">
                                                    <div style="float: left; width: 25%;">
                                                        <div>  <br />
                                                         <img src="images/TCS_Logo.png" alt="TCS Logo" />
                                                           <%-- <img src="images/mini_logo.png" alt="Logo" />--%>
                                                            <div>
                                                            </div>
                                                           <%-- <div>
                                                                The City School
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <div style="float: right; width: 75%;">
                                                        <div style="text-align: right;">
                                                            <div>
                                                                <asp:Literal ID="ltFullName" runat="server" Text='<%#Eval("FullName")%>'></asp:Literal>
                                                            </div>
                                                            <div>
                                                                <asp:Literal ID="Literal8" runat="server" Text='<%#Eval("DesigName")%>'></asp:Literal>
                                                            </div>
                                                          <%--  <div>
                                                                <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("Other")%>'></asp:Literal>
                                                            </div>--%>
                                                            <div>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <br />
                                                        </div>
                                                        <div style="text-align: left;">
                                                            <div>
                                                                <asp:Literal ID="Literal15" runat="server" Text='<%#Eval("address")%>'></asp:Literal>
                                                            </div>
                                                            <div>
                                                                UAN:<asp:Literal ID="Literal10" runat="server" Text='<%#Eval("UAN")%>'></asp:Literal>
                                                            </div>
                                                            <div>
                                                                FAX:
                                                                <asp:Literal ID="Literal11" runat="server" Text='<%#Eval("Fax")%>'></asp:Literal>
                                                            </div>
                                                            <div>
                                                                Cell:
                                                                <asp:Literal ID="Literal12" runat="server" Text='<%#Eval("ContactNumber")%>'></asp:Literal>
                                                                Email:
                                                                <asp:Literal ID="Literal13" runat="server" Text='<%#Eval("Email")%>'></asp:Literal>
                                                            </div>
                                                            <div>
                                                                <asp:Literal ID="Literal14" runat="server" Text='<%#Eval("Web")%>'></asp:Literal>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="FullName" />
                                        <asp:BoundField DataField="DesigName" HeaderText="Designation" SortExpression="DesigName" />
                              <%--  
                                <asp:BoundField DataField="ContactNumber" HeaderText="Contact #"></asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                                <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                                
                                <asp:BoundField DataField="Other" HeaderText="Other" SortExpression="Other" />--%>
                                 <asp:BoundField DataField="EmpBCard_Id" HeaderText="Request #" SortExpression="EmpBCard_Id"   />
                                 <asp:BoundField DataField="CreatedOn" HeaderText="Requested On" SortExpression="CreatedOn" DataFormatString="{0:dd/MMM/yyyy}" />
                                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                              <%--  <asp:BoundField DataField="Cost" HeaderText="Unit Cost" SortExpression="Cost" DataFormatString="{0:N2}" />
                              
                                <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                <asp:Label runat="server" id="lblAmount"   >  <%# String.Format("{0:N2}", ( Convert.ToInt32(Eval("Quantity")) * Convert.ToDecimal(Eval("Cost")))) %> </asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:BoundField DataField="Remarks" HeaderText="Reason" SortExpression="Remarks" />--%>
                          
                                 <asp:TemplateField HeaderText="Remarks"  Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" ></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle  />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Preview"  >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("EmpBCard_Id") %>'
                                                    OnClick="btnPrint_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print" ImageUrl="~/images/Printer.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnSelectChecked" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                              
                                                   
                                                  

                               
                            
                            </Columns>
                            <RowStyle CssClass="tr1" />
                            <HeaderStyle CssClass="tableheader" />
                            <AlternatingRowStyle CssClass="tr2" />
                            <SelectedRowStyle CssClass="tr_select" />
                        </asp:GridView>
                    </div>
                    <!--end panel_body-->
                    <div class="panel_footer">
                      <div class="save_btn">
                                            <asp:Button runat="server" ID="btnSave" class="round_corner btn" Text="Mark As Request Completed"
                                                OnClick="btnSave_Click" />
                                                 <asp:Button runat="server" ID="btnPrintAll" class="round_corner btn" Text="Preview Selected"
                                                OnClick="btnPrintAll_Click" />
                                        </div>
                        <!--end panel_footer-->
                    </div>
                </div>
            </div>
            <!--end inner wrap-->
        </div>
    </div>
          </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

