<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeShifts_SpecialCases_WorkingDay.aspx.cs" Inherits="EmployeeShifts_SpecialCases_WorkingDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript">


                $(document).ready(document_Ready);

                function document_Ready() {


                    /*Date Picker*/
                    $(function () {
                        $(".datepicker").datepicker({
                            dateFormat: "mm/dd/yy"
                        });
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
                    <h1 class="page_heading">Mark Working Day For Employees</h1>
                    <div class="controls round_corner">
                        <div>
                            <asp:LinkButton ID="btnAddShift" runat="server" CssClass="btn round_corner" CausesValidation="False"
                                Font-Bold="False" OnClick="btnAddShift_Click">Add New</asp:LinkButton>
                        </div>
                    </div>
                    <div style="padding: 2px;">
                        <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" Enabled="false" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" Enabled="true" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
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
                                OnSorting="gvEmployees_Sorting" OnPageIndexChanging="gvEmployees_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="WorkingDay_Id" HeaderText="WorkingDay_Id" SortExpression="WorkingDay_Id"
                                        Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="Center" HeaderText="Center Name" SortExpression="Center" />
                                    <asp:BoundField DataField="WorkingDate" HeaderText="Working Date" SortExpression="WorkingDate"
                                        DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("WorkingDay_Id") %>'
                                                OnClick="btnEdit_Click" ForeColor="#004999" Style="text-align: center; font-weight: bold;"
                                                ToolTip="Edit" CssClass="glyphicon glyphicon-pencil"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("WorkingDay_Id") %>'
                                                OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure to perform this action?')"
                                                ForeColor="#004999" Style="text-align: center; font-weight: bold;" ToolTip="Delete"
                                                CssClass="glyphicon glyphicon-trash"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
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
                    <asp:Panel ID="pAddNew" runat="server" class="panel" Visible="false">
                        <div class="form-group">
                            <div class="panel_head">
                                <asp:Label ID="blheading2" runat="server" Text=" Set  Employee Working Day"></asp:Label>
                            </div>

                            <asp:Label runat="server" ID="lblAttdate" for="Attendance Date: " ForeColor="white"
                                class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">*Attendance Date:</asp:Label>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                                <asp:TextBox ID="txtDate" runat="server" class="control-label textbox datepicker" placeholder="Attendance Date"
                                    CausesValidation="true">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    Display="Dynamic" ErrorMessage="Date Required" SetFocusOnError="True" ForeColor="white"
                                    ValidationGroup="c"></asp:RequiredFieldValidator>
                            </div>

                            <asp:Label runat="server" ID="lblRemarks" ForeColor="white" class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Remarks*:
                            </asp:Label>
                            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="col-lg-8 col-md-8 col-sm-8 col-xs-12 control-label textbox"
                                    TextMode="MultiLine" Rows="3">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemarks"
                                    Display="Dynamic" ErrorMessage="Remarks are Required" SetFocusOnError="True" ForeColor="white"
                                    ValidationGroup="c">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group">
                            <asp:CheckBox runat="server" ID="cbApplyCenter" AutoPostBack="true" Text="Apply to Centers" ForeColor="white" />
                            <br /><br /><br />
                            <asp:CheckBox ID="cb_GenderSpecific" runat="server" Text="Gender Specific * " ForeColor="white"
                                Checked="false" AutoPostBack="true" OnCheckedChanged="cb_GenderSpecific_CheckedChanged" />

                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12 form-group" runat="server" id="divGender"
                            visible="false">
                            <asp:Label runat="server" ID="lblGender" for="Gender Specific: " ForeColor="white"
                                class=" col-lg-2 col-md-2 col-sm-2 col-xs-12 control-label">Gender*:</asp:Label>
                            <asp:DropDownList ID="ddlGender" runat="server" Width="323px" Enabled="true" AutoPostBack="True">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-12 text-right">
                            <div class="pull-right">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="c" class="btn btn-success"
                                    CausesValidation="true" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                </div>
                </asp:Panel>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
