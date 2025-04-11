<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResignationTerminationReversal.aspx.cs" Inherits="ResignationTerminationReversal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <script type="text/javascript"> 

        $(document).ready(document_Ready);

        /*Date Picker*/
        $(function () {
            $('.datepicker').datepicker({
                dateFormat: "mm/dd/yy",
                minDate: 0
            });
            $("#anim").change(function () {
                $(".datepicker").datepicker("option", "showAnim", $(this).val());
            });
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $('.datepicker').datepicker();
        });

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
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
            <div class="fullrow">
                <div class="inner_wrap">
                    <div class="panel">
                        <div class="panel_head">
                            Resignation/Termination Reversal
                        </div>
                        <div class="form-horizontal">
                            <div class="new_event">
                                <div class="row">
                                    <div class="form-group col-xl-12" style="display: flex;">
                                        <span class="LabelWhite"></span>
                                        <asp:TextBox ID="txtUser" runat="server" MaxLength="50" CssClass="form-control" placeholder="Type Employee Code" OnKeyPress="return preventEnterKey(event);"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                            CssClass="btn btn-primary" Text="Search" Width="10%" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="body_content fullrow">
                <div class="inner_wrap">
                    <asp:UpdatePanel ID="upResignations" runat="server">
                        <%--Resignation section--%>
                        <ContentTemplate>
                            <%-- ****************************** Resignation UnApproved records section  *****************************--%>
                            <div class="panel" id="div_ResignationsUnApproved" runat="server">
                                <div class="panel_head">
                                    <p>
                                        Employee Information
                                    </p>
                                </div>
                                <!--end panel_head-->
                                <div class="panel_head" style="font-size: 13px; color: yellow;">
                                    The highlighted column in gridview is editable!
                                </div>
                                <div class="panel_body">
                                    <!--Paste Grid code here-->
                                    <asp:GridView ID="gvResignationTermination" SkinID="GridView" runat="server" Width="100%"
                                        AutoGenerateColumns="False" HorizontalAlign="Center" EmptyDataText="No Data Exist!">
                                        <Columns>
                                            <asp:BoundField DataField="FullName" HeaderText="Name">
                                                <ItemStyle Width="210px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                                            <asp:BoundField DataField="DesigName" HeaderText="Designation"></asp:BoundField>
                                            <asp:BoundField DataField="DeptName" HeaderText="Department"></asp:BoundField>
                                            <asp:BoundField DataField="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:MM/dd/yyyy}"
                                                HtmlEncode="False">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Reason" HeaderText="Category"></asp:BoundField>
                                            <asp:BoundField DataField="HOD" HeaderText="HOD">
                                                <ItemStyle Width="210px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" DataFormatString="{0:MM/dd/yyyy}"
                                                HtmlEncode="False">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="HR Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                        MaxLength="250"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Working Date">
                                                <ItemTemplate>
                                                    <div style="background-color: yellow">
                                                        <asp:TextBox ID="txtLastWorkingDate" runat="server" Text='<%# Bind("LastWorkingDate", "{0:MM/dd/yyyy}") %>' CssClass="datepicker" />
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!--end panel_body-->

                                <div class="panel_footer">
                                    <div class="save_btn">
                                        <asp:Button runat="server" ID="btnUpdateLastWorkingDate" class="round_corner btn" Text="Update Last Working Date"
                                            OnClick="btnUpdateLastWorkingDate_Click" Visible="false" />
                                    </div>
                                    <div class="save_btn">
                                        <asp:Button runat="server" ID="btnReverseEmployeeResignationTermination" class="round_corner btn" Text="Reverse Resignation/Termination"
                                            OnClick="btnReverseEmployeeResignationTermination_Click" Visible="false" />
                                    </div>
                                </div>
                                <!--end panel_footer-->
                            </div>
                            <!--end panel-->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="clear">
                    </div>
                </div>
                <!--end inner_wrap-->
            </div>
            <!--end body_content-->
            </div>
            <!--end outer_wrap-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>