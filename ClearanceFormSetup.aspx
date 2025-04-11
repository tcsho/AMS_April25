<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClearanceFormSetup.aspx.cs" Inherits="ClearanceFormSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style type="text/css">
        .new_event select {
            width: 50%;
            height: 40px;
            border: #FFF solid 1px;
            margin-bottom: 10px;
            display: block;
            background: #fff;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="fullrow">
                <div class="inner_wrap">
                    <div class="panel">
                        <div class="panel_head">
                            Clearance Form Setup
                        </div>
                        <div class="form-horizontal">
                            <div class="new_event">
                                <div class="row">
                                    <div class="form-group col-xl-12" style="display: flex; justify-content: space-between; align-items: center; gap: 10px;">
                                        <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="form-control" Style="flex: 1;">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" Style="flex: 2;">
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtUser" runat="server" MaxLength="50" CssClass="form-control" placeholder="Type Employee Code" Style="flex: 2;" />--%>                                        
                                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="round_corner btn" Text="Save" Width="10%" Style="flex-shrink: 0;" />
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
                                        Department Wise Employee Listing
                                    </p>
                                </div>
                                <!--end panel_head-->
                                <div class="panel_body">
                                    <!--Paste Grid code here-->
                                    <asp:GridView ID="gvEmpInfoDptWise" SkinID="GridView" runat="server" Width="100%"
                                        AutoGenerateColumns="False" HorizontalAlign="Center" EmptyDataText="No Data Exist!" OnRowCommand="gvEmpInfoDptWise_RowCommand" DataKeyNames="Id">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode"></asp:BoundField>
                                            <asp:BoundField DataField="FullName" HeaderText="FullName"></asp:BoundField>
                                            <asp:BoundField DataField="Department" HeaderText="Department"></asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" ButtonType="Link"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <!--end panel_body-->
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