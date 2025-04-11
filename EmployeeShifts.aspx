<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeShifts.aspx.cs" Inherits="EmployeeShifts" %>

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
                            <asp:DropDownList ID="ddlMonths" runat="server"  Width="130px"
                                 AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEmp" runat="server"  Width="323px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button ID="btnChangeShift" runat="server" CssClass=" btn round_corner "
                                Text="Change Shift" Font-Names="Arial" Font-Size="11px" CausesValidation="false" OnClick="btnChangeShift_Click"
                                Style="display: none" />
 
                        </div>




                <asp:Panel ID="pan_New" runat="server"  class="panel new_event_wraper">

                            <div class="new_event">
               
               <%--Change Timings--%>

                    <p>Start Time*:</p>
                                   
                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="jtimepicker"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Start Time"
                        ControlToValidate="txtStartTime"></asp:RequiredFieldValidator>



                    <p>End Time*:</p>

                     <asp:TextBox ID="txtEndTime" runat="server" CssClass="jtimepicker"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter End Time"
                        ControlToValidate="txtEndTime"></asp:RequiredFieldValidator>



                    <p>Absent Time*:</p>

                    <asp:TextBox ID="txtAbsentTime" runat="server" CssClass="jtimepicker"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Absent Time"
                        ControlToValidate="txtAbsentTime"></asp:RequiredFieldValidator>


                    <asp:Button ID="btnSaveShift" runat="server" Text="Save" OnClick="btnSaveShift_Click" />
                    <asp:Button ID="btnCancelShift" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancelShift_Click" />


                
            </div>

            </asp:Panel>




                        <div class="panel">
                            <div class="panel_head">
                                Manage Employee Shifts
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height:100%; overflow:auto;">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvShifts" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    EmptyDataText="No record found." HorizontalAlign="Center" OnPageIndexChanging="gvShifts_PageIndexChanging"
                                    OnRowDataBound="gvShifts_RowDataBound" OnSorting="gvShifts_Sorting" OnRowCommand="gvShifts_RowCommand"
                                    AllowPaging="True" AllowSorting="True" PageSize="200">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="8px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="employeecode" SortExpression="employeecode" HeaderText="Code">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0: dddd MM/dd/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActSTime" SortExpression="ActSTime" HeaderText="Start Time">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ActETime" SortExpression="ActETime" HeaderText="End Time">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AbsentTime" SortExpression="AbsentTime" HeaderText="Absent Time">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="cb">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Check</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Copy">
                                                <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ForeColor="#004999" OnClick="btnEdit_Click"
                                                        Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("EmployeeCode") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>     --%>


                                            <%--<asp:TemplateField HeaderText="Re-Set">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnResetLeaves" runat="server" ForeColor="#004999" OnClick="btnResetLeaves_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Re-Set" ImageUrl="~/images/passrest.png"
                                                            CommandArgument='<%# Eval("employeecode") %>'  OnClientClick="return confirm('Are you sure to Re-Set submission and approval of this day?');" >
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>--%>


                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <!--end panel_footer-->
                            </div>
                        </div>
                        <%--<table cellpadding="0" cellspacing="0" width="100%" style="height: auto">
                            <tr style="font-size: x-large; width=100%;" align="center">
                                <td>
                                   
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <table id="grid" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titlesection" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="5" style="width: 100%">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>--%>
                        <%--<cc1:ModalPopupExtender ID="MPEShift" runat="server" TargetControlID="btnChangeShift"
                            PopupDragHandleControlID="popupdrag" CancelControlID="btnCancelShift" PopupControlID="pnlShift">
                        </cc1:ModalPopupExtender>--%>
                        
                    </div>
                </div>
            </div>




            <script type="text/javascript">

                $(document).ready(document_Ready);

                function document_Ready() {
                    $('.jtimepicker').timepicker({ 'timeFormat': 'H:i:s' });
                    
                }

                //Re-bind for callbacks
                var prm = Sys.WebForms.PageRequestManager.getInstance();

                prm.add_endRequest(function () {
                    document_Ready();
                }); 
                
                


            </script>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
