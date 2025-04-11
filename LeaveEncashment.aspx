<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveEncashment.aspx.cs" Inherits="LeaveEncashment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="body_content fullrow">
                <div class="inner_wrap">
                    <asp:UpdatePanel ID="upResignations" runat="server">
                        <%--Resignation section--%>
                        <ContentTemplate>
                            <%-- ****************************** Resignation UnApproved records section  *****************************--%>
                            <div class="panel" id="div_ResignationsUnApproved" runat="server">
                                <div class="panel_head">
                                    <p>
                                        Encashment of Leave
                                    </p>
                                </div>
                                <!--end panel_head-->
                                <div class="panel_body">
                                    <!--Paste Grid code here-->
                                    <asp:GridView ID="gvLeaveEncashment" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered" OnRowCommand="gvLeaveEncashment_RowCommand" OnRowDataBound="gvLeaveEncashment_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="EmpLeave_Id" HeaderText="EmpLeave_Id" SortExpression="EmpLeave_Id" Visible="False" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfEmpLeaveId" runat="server" Value='<%# Eval("EmpLeave_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                            <asp:BoundField DataField="Region_Name" HeaderText="Region" SortExpression="Region_Name" />
                                            <asp:BoundField DataField="LeaveEncashmentDays" HeaderText="Leave Encashment Days" SortExpression="LeaveEncashmentDays" />
                                            <asp:BoundField DataField="ReportTo" HeaderText="HOD" SortExpression="ReportTo" />
                                            <asp:BoundField DataField="HOD" HeaderText="HOD" SortExpression="HOD" />
                                            <asp:BoundField DataField="HODEmail" HeaderText="HOD Email" SortExpression="HODEmail" Visible="False" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfhodEmail" runat="server" Value='<%# Eval("HODEmail") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="HODRemakrs" HeaderText="HOD Approval Remarks" SortExpression="HODRemakrs" />
                                            <asp:BoundField DataField="Submit2BOD" HeaderText="Submit To BOD" SortExpression="Submit2BOD" />
                                            <asp:BoundField DataField="BODApprove" HeaderText="BOD Approval" SortExpression="BODApprove" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnReject" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CommandName="reject" Text="Reject" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSubmitToBoD" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CommandName="submitbod" Text="Submit to BoD" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnResubmit" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CommandName="resubmit" Text="Resubmit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id" Visible="False" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfEmpRegion_Id" runat="server" Value='<%# Eval("Region_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id" Visible="False" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfEmpCenter_Id" runat="server" Value='<%# Eval("Center_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="gvLeaveEncashmentHoD" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-bordered" OnRowCommand="gvLeaveEncashmentHoD_RowCommand" OnRowDataBound="gvLeaveEncashmentHoD_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="EmpLeave_Id" HeaderText="EmpLeave_Id" SortExpression="EmpLeave_Id" Visible="False" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfEmpLeaveId" runat="server" Value='<%# Eval("EmpLeave_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                            <asp:BoundField DataField="LeaveEncashmentDays" HeaderText="Leave Encashment Days" SortExpression="LeaveEncashmentDays" />
                                            <asp:BoundField DataField="ReportTo" HeaderText="Line Manager Code" SortExpression="ReportTo" />
                                            <asp:BoundField DataField="HOD" HeaderText="Line Manager Name" SortExpression="HOD" />
                                            <asp:BoundField DataField="HODRemakrs" HeaderText="Line Manager Approval Remarks" SortExpression="HODRemakrs" />
                                            <asp:BoundField DataField="EncashmentSubmitToHR" HeaderText="Submit To HR" SortExpression="EncashmentSubmitToHR" />
                                            <asp:TemplateField HeaderText="HOD Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemarks" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                        MaxLength="250"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="500px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSubmitToHR" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CommandName="submithr" Text="Submit to HR" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!--end panel_body-->
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