<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AlternateDaysWorking.aspx.cs" Inherits="AlternateDaysWorking" %>


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
                            <asp:DropDownList ID="ddlMonths" runat="server" Width="130px" Enabled="false" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="323px" AutoPostBack="True"
                                Enabled="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlCenter" runat="server" Width="323px" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <div style="clear: both;">
                            </div>
                            <asp:Button ID="btnAddNewAlternateDaysWorking" runat="server" CssClass=" btn round_corner" Text="Add New Alternate Working Day"
                                Font-Names="Arial" Font-Size="11px" CausesValidation="false" OnClick="btnAddNewAlternateDaysWorking_Click"
                                Style="float: right;" />
                            
                        </div>
                        <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper">
                            <div class="new_event">
                                <%--Change Timings--%>
                                <p>
                                    Off Day:</p>
                                <asp:TextBox ID="txtOffDay" runat="server" MaxLength="30" CssClass="datepicker"></asp:TextBox>
                                <p>
                                    Alternate Working Day:</p>
                                <asp:TextBox ID="txtAlternateWorkingDay" runat="server" MaxLength="30" CssClass="datepicker"></asp:TextBox>
                                <p>
                                Reason:
                                </p>
                                <asp:TextBox ID="txt_Reason" runat="server" ></asp:TextBox>
                                

                                <asp:Button ID="btnSaveVacation" runat="server" Text="Save" OnClick="btnSaveVacation_Click" CausesValidation="true" />
                                <asp:Button ID="btnCancelVacation" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancelVacation_Click" />
                            </div>
                        </asp:Panel>
                        <div class="panel">
                            <div class="panel_head">
                                Alternate Working Days.
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body" style="max-height: 100%; overflow: auto;">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvShifts" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    EmptyDataText="No record found." HorizontalAlign="Center" OnPageIndexChanging="gvShifts_PageIndexChanging"
                                    OnRowDataBound="gvShifts_RowDataBound" OnSorting="gvShifts_Sorting" OnRowCommand="gvShifts_RowCommand"
                                    AllowPaging="false" AllowSorting="True" >
                                    <Columns>
                                        <asp:BoundField DataField="AlternateWorkingDays_id" HeaderText="AlternateWorkingDays_id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemStyle Width="20px" />
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Off_day" SortExpression="Off_day" HeaderText="Off Day"
                                            DataFormatString="{0: dddd MM/dd/yyyy}">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Alternate_working_day" SortExpression="Alternate_working_day" HeaderText="Alternate Working Day"
                                            DataFormatString="{0: dddd MM/dd/yyyy}">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">    
                                        </asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <!--end panel_footer-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                $(document).ready(document_Ready);

                function document_Ready() {
                    $('.jtimepicker').timepicker({ 'timeFormat': 'H:i:s' });



                    /*Date Picker*/
                    $(function () {
                        $(".datepicker").datepicker();
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>