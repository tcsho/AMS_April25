<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeBusinessCard.aspx.cs" Inherits="EmployeeBusinessCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                 
 
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Business Card Order Form</p>
                        <div class="">
                            <div style="float:left">
                                <asp:LinkButton ID="btnAddRequest" OnClick="btnAddRequest_Click" runat="server" CssClass="btn round_corner"
                                    CausesValidation="false" Font-Bold="False">Add New Order Request</asp:LinkButton>
                            </div>
                            <div style="float:right;" runat="server" id="divRequestForPrinting">
                             
                              <asp:LinkButton ID="btnRequestForPrinting" OnClick="btnRequestForPrinting_Click" runat="server" 
                             CssClass="btn round_corner"
                              
                              CausesValidation="false" Font-Bold="False">Approved Requests for Printing
                                    </asp:LinkButton>
                             </div>
                        </div>

                        

                        <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper " Style="display: inline;">
                            <div class="new_event">
                                <p>
                                    *Name :</p>
                                <asp:TextBox ID="txtFName" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFName"
                            Display="Dynamic" ErrorMessage="First Name Required" SetFocusOnError="True" ValidationGroup="c"></asp:RequiredFieldValidator>--%>
                                <p>
                                    *Designation :</p>
                                <asp:TextBox ID="txtDesignation" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                <p>
                                    Cell #:
                                </p>
                                <asp:TextBox ID="txtCell" runat="server"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCell"
                                    Display="Dynamic" ErrorMessage="Cell # is required" SetFocusOnError="True" ValidationGroup="c"></asp:RequiredFieldValidator>--%>
                                <p>
                                    *Email :
                                </p>
                                <asp:TextBox ID="txtEmail" runat="server" ViewStateMode="Enabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                    Display="Dynamic" ErrorMessage="Email address is required" SetFocusOnError="True"
                                    ValidationGroup="c"></asp:RequiredFieldValidator>
                               <%-- <p>
                                    Other :
                                </p>--%>
                                <asp:TextBox ID="txtOther" runat="server" Visible="false"></asp:TextBox>
                                <p>
                                    *Quantity :</p>
                                <asp:DropDownList ID="ddlQuantity" runat="server" Enabled="false">
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>250</asp:ListItem>
                                    <asp:ListItem>500</asp:ListItem>
                                    <asp:ListItem>1000</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="txtQuantity" runat="server"  ></asp:TextBox>--%>
                                <div class="hide">
                                    <p>
                                        *Cost :</p>
                                    <asp:TextBox ID="txtCost" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <%--  <asp:DropDownList id="ddlCost" runat="server">
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>2.5</asp:ListItem>
                            <asp:ListItem  Selected="True" Text="3.5" Value="3.5" >3.5</asp:ListItem>
                            </asp:DropDownList>--%>
                                    <p>
                                        *Address :
                                    </p>
                                    <asp:TextBox ID="txtAddress" runat="server" ReadOnly="true" Enabled="false" TextMode="MultiLine"
                                        Width="50%"></asp:TextBox>
                                    <p>
                                        *UAN #:</p>
                                    <asp:TextBox ID="txtUAN" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <p>
                                        *Fax #:</p>
                                    <asp:TextBox ID="txtFax" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <p>
                                        *Web :</p>
                                    <asp:TextBox ID="txtWeb" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                </div>
                               <%-- <p>
                                    Remarks :
                                </p>--%>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="50%" Visible="false"></asp:TextBox>
                                <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="button"
                                    ValidationGroup="c" Text="Save"></asp:Button>
                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" CausesValidation="False"
                                    Text="Cancel"></asp:Button>
                            </div>
                        </asp:Panel>
                        <div class="panel">
                            <div class="panel_head">
                                List of Business Card Requests
                            </div>
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <asp:GridView ID="gv" runat="server"  
                                Width="100%" AutoGenerateColumns="False" SkinID="GridView"
                                    HorizontalAlign="Center" OnPageIndexChanging="gv_PageIndexChanging" OnRowDeleting="gv_RowDeleting"
                                    OnSorting="gv_Sorting" OnRowDataBound="gv_RowDataBound" AllowPaging="True" AllowSorting="True"
                                    PageSize="10" EmptyDataText="No Data Exist!">
                                    <Columns>
                                        <asp:BoundField DataField="EmpBCard_Id" HeaderText="Id" SortExpression="EmpBCard_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sr.#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:BoundField DataField="Region_Name" HeaderText="Region Name" SortExpression="Region_Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Center_Name" HeaderText="Center Name" SortExpression="Center_Name">
                                        </asp:BoundField> --%>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sample Card" Visible="false">
                                            <ItemTemplate>
                                                <div style="width: 100%;">
                                                    <div style="float: left; width: 25%;">
                                                        <div>
                                                        <br />
                                                            <img src="images/TCS_Logo.png" alt="TCS Logo" />
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
                                                            <div>
                                                                <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("Other")%>'></asp:Literal>
                                                            </div>
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
                                        <%--<asp:BoundField DataField="ContactNumber" HeaderText="Contact #"></asp:BoundField>
                                        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                                        <asp:BoundField DataField="deptName" HeaderText="Department" SortExpression="deptName" />
                                        
                                        <asp:BoundField DataField="Other" HeaderText="Other" SortExpression="Other" />--%>
                                         <asp:BoundField DataField="EmpBCard_Id" HeaderText="Request #" SortExpression="EmpBCard_Id"   />
                                         <asp:BoundField DataField="CreatedOn" HeaderText="Requested On" SortExpression="CreatedOn" DataFormatString="{0:dd/MMM/yyyy}" />
                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                        <asp:TemplateField HeaderText="HOD" SortExpression="HOD_EmpBCardStatus_Id" >
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" CssClass="glyphiconNoChange glyphicon glyphicon-minus-sign"
                                                    Visible='<%# Eval("HOD_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("HOD_EmpBCardStatus_Id"))== 1 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal1" runat="server" CssClass="glyphiconLightGreen glyphicon glyphicon-ok-sign"
                                                    Visible='<%# Eval("HOD_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("HOD_EmpBCardStatus_Id"))== 2 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal2" runat="server" CssClass="glyphiconRed glyphicon glyphicon-remove-sign"
                                                    Visible='<%# Eval("HOD_EmpBCardStatus_Id") != DBNull.Value && Convert.ToInt32(Eval("HOD_EmpBCardStatus_Id"))== 3 ?true:false%>'>        </asp:Label>
                                                <%-- <asp:Label runat="server" id="lbl" Text='<%# Eval("isVerified") != DBNull.Value && Convert.ToBoolean(Eval("isVerified"))==true?true:false%>' CssClass="hide"></asp:Label>--%>
                                                <%-- <asp:Image ID="btnScanTick" runat="server" ForeColor="#004999" Style="text-align: center;
                                                font-weight: bold;" ImageUrl="~/images/Scan_tick.png" Visible='<%# Convert.ToBoolean(Eval("isVerified"))==true?true:false%>' />
                                            <asp:Image ID="btnScanCross" runat="server" ForeColor="#004999" Style="text-align: center;
                                                font-weight: bold;" ImageUrl="~/images/Scan_Cross.png" Visible='<%# Convert.ToBoolean(Eval("isVerified"))==false?true:false%>' />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HR/RD" SortExpression="HR_RD_EmpBCardStatus_Id" Visible="false">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" CssClass="glyphiconNoChange glyphicon glyphicon-minus-sign"
                                                    Visible='<%# Eval("HR_RD_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("HR_RD_EmpBCardStatus_Id"))== 1 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal3" runat="server" CssClass="glyphiconLightGreen glyphicon glyphicon-ok-sign"
                                                    Visible='<%# Eval("HR_RD_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("HR_RD_EmpBCardStatus_Id"))== 2 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal4" runat="server" CssClass="glyphiconRed glyphicon glyphicon-remove-sign"
                                                    Visible='<%# Eval("HR_RD_EmpBCardStatus_Id") != DBNull.Value && Convert.ToInt32(Eval("HR_RD_EmpBCardStatus_Id"))== 3 ?true:false%>'>        </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CEO" SortExpression="CEO_EmpBCardStatus_Id" Visible="false">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" CssClass="glyphiconNoChange glyphicon glyphicon-minus-sign"
                                                    Visible='<%# Eval("CEO_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("CEO_EmpBCardStatus_Id"))== 1 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal5" runat="server" CssClass="glyphiconLightGreen glyphicon glyphicon-ok-sign"
                                                    Visible='<%# Eval("CEO_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("CEO_EmpBCardStatus_Id"))== 2 ?true:false%>'>  </asp:Label>
                                                <asp:Label ID="literal6" runat="server" CssClass="glyphiconRed glyphicon glyphicon-remove-sign"
                                                    Visible='<%# Eval("CEO_EmpBCardStatus_Id") != DBNull.Value && Convert.ToInt32(Eval("CEO_EmpBCardStatus_Id"))== 3 ?true:false%>'>        </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="HOD_EmpBCardStatus_Id" HeaderText="HOD" SortExpression="HOD_EmpBCardStatus_Id" />
                                <asp:BoundField DataField="HR_RD_EmpBCardStatus_Id" HeaderText="HR / RD" SortExpression="HR_RD_EmpBCardStatus_Id" />
                                <asp:BoundField DataField="CEO_EmpBCardStatus_Id" HeaderText="CEO" SortExpression="CEO_EmpBCardStatus_Id" />--%>
                                        <asp:TemplateField HeaderText="Preview"  >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnPrint" runat="server" CommandArgument='<%# Eval("EmpBCard_Id") %>'
                                                    OnClick="btnPrint_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    ToolTip="Print" ImageUrl="~/images/Printer.png"></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received" SortExpression="EmpBCardStatus_Id">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%--<asp:Label ID="Label1" runat="server" CssClass="glyphiconNoChange glyphicon glyphicon-remove-sign"
                                            Visible='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id")) != 5?true:false%>'>  </asp:Label>--%>
                                                <asp:Label ID="Label4" runat="server" CssClass="glyphiconNoChange glyphicon glyphicon-minus-sign"
                                                    Visible='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id"))<4 ?true:false%>'>  </asp:Label>
                                                <asp:Button ID="btnRceived" runat="server" CommandArgument='<%# Eval("EmpBCard_Id") %>'
                                                    OnClick="btnReceived_Click" Text="Verify Receiving"
                                                     Visible='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id"))== 6?true:false%>' />
                                                <asp:Label ID="literal7" runat="server" CssClass="glyphiconLightGreen glyphicon glyphicon-ok-sign"
                                                    Visible='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id"))==5 ?true:false%>'>  </asp:Label>
                                                <%--  <asp:ImageButton ID="btnReceived" runat="server" CommandArgument='<%# Eval("EmpBCard_Id") %>'
                                                    OnClick="btnReceived_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                    Visible='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id"))== 5 ?true:false%>'
                                                    Enabled='<%# Eval("EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("EmpBCardStatus_Id"))== 5 ?false:true%>'
                                                    ToolTip="Received" ImageUrl="~/images/tick.png"></asp:ImageButton>--%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    ImageUrl="~/images/delete.gif" Text="" 
                                                    Visible='<%# Eval("HOD_EmpBCardStatus_Id") != DBNull.Value &&  Convert.ToInt32 (Eval("HOD_EmpBCardStatus_Id"))== 1 ?true:false%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Region_Id" HeaderText="Region_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Center_Id" HeaderText="Center_Id">
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
                            <!--end panel_body-->
                            <div class="panel_footer">
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
