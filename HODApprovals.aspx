<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HODApprovals.aspx.cs" Inherits="HODApprovals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //function BindEvents() {
        //$(document).ready(function () {

        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            //            $('.close').click(function () {
            //                $('.mesg').slideUp("slow");
            //                $('.error_mesg').slideUp("slow");
            //            });



            //             /*********Pnael_expand***********/
            //             $('.panel_head').append('<span class="panel_expand">Plus icon</span>');
            //             $('.panel_expand').click(function () {
            //                 var parentpanel = $(this).parents(".panel");
            //                 $(parentpanel).css("background", "#C60");
            //                 $(parentpanel).find('.panel_body').css("max-height", "100%");
            //                 /*	alert("click");
            //                 $('.panel_body').css("max-height","100%");*/
            //             });


            //             /*********panel_expand***********/



        }

        //Re-bind for callbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            document_Ready();
        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <%-- ****************************** drop down section  *****************************--%>
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="controls round_corner">
                            <div class="show_hide_btn btn round_corner">
                                <p>
                                    Show/Hide Approved
                                </p>
                            </div>
                            <%--<asp:DropDownList ID="ddlLeavesStatus" runat="server" CssClass="dropdownlist" Width="130px"
                                                    AutoPostBack="True" 
                        onselectedindexchanged="ddlLeavesStatus_SelectedIndexChanged" >

                        <asp:ListItem Text="">
                        </asp:ListItem>
                                                </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist" Width="130px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEmp" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <!--end controls-->
                        <div class="clear">
                        </div>
                    </div>
                    <!--end inner wrap-->
                </div>
                <!--end fullrow-->
                <div class="body_content fullrow">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="upLateArrivals" runat="server">
                            <%--Late Arrivals records--%>
                            <ContentTemplate>
                                <%-- ****************************** LateArrivals UnApproved records section  *****************************--%>
                                <div class="panel" id="div_lteUnApproved" runat="server">
                                    <div class="panel_head">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <asp:Panel ID="Panel1" runat="server" CssClass="Pbar">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loading.gif" />
                                                </asp:Panel>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <cc1:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
                                            TargetControlID="UpdateProgress1" HorizontalSide="Center" VerticalSide="Middle"
                                            HorizontalOffset="0" VerticalOffset="100">
                                        </cc1:AlwaysVisibleControlExtender>
                                        <p>
                                            Late Arrivals Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--Paste Grid code here-->
                                        <asp:GridView ID="gvLateArrivalsUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvLateArrivalsUnApproved_PageIndexChanging"
                                            OnRowDataBound="gvLateArrivalsUnApproved_RowDataBound" OnSorting="gvLateArrivalsUnApproved_Sorting"
                                            OnRowCommand="gvLateArrivalsUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegAttSubmit2HOD" HeaderText="NegAttSubmit2HOD">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                                    DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegHODSubmit" SortExpression="NegHODSubmit" HeaderText="Submit">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Negative Type">
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegEmpReason" SortExpression="NegEmpReason" HeaderText="Employee Reason">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAprvLateArrivals" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlAprvLateArrivals_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="40px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRoleTypeLateArrivals" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlRoleTypeLateArrivals_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="100px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkLateArrivals" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnLateArrivalsChecked" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkLateArrivals" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditLateArrivals" runat="server" ForeColor="#004999" OnClick="btnEditLateArrivals_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="ReSubmit">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReSubmitLateArrivals" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                            ImageUrl="~/images/icon_go.gif" OnClick="btnReSubmitLateArrivals_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                    <div class="panel_footer">
                                        <p>
                                            Total :
                                            <asp:Label ID="lblLateArrivalsTotal" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Submission :
                                            <asp:Label ID="lblLateArrivalsNotSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Submitted :
                                            <asp:Label ID="lblLateArrivalsSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Approved :
                                            <asp:Label ID="lblLateArrivalsApproved" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Approval :
                                            <asp:Label ID="lblLateArrivalsNotApproved" runat="server"></asp:Label>
                                        </p>
                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnLateArrivalsSave" class="round_corner btn" Text="Save"
                                                OnClick="btnLateArrivalsSave_Click" />
                                        </div>
                                        <%-- <div class="error_mesg round_corner">Error Data saving.
                        <img class="close" src="images/cross.png" />
                    </div><!--end mesg-->
                      <div class="mesg round_corner">Records have been saved.
                        	<img class="close" src="images/cross.png" />
                        </div><!--end mesg-->--%>
                                    </div>
                                    <!--end panel_footer-->
                                </div>
                                <!--end panel-->
                                <%-- ****************************** LateArrivals Approved records section  *****************************--%>
                                <div class="panel approved" id="div_lteApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Late Arrivals [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--paste grid code here-->
                                        <asp:GridView ID="gvLateArrivalsApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvLateArrivalsApproved_PageIndexChanging"
                                            OnSorting="gvLateArrivalsApproved_Sorting" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                                    DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                                    <ItemStyle Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegHODSubmit" SortExpression="NegHODSubmit" HeaderText="Submit">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegEmpReason" SortExpression="NegEmpReason" HeaderText="Employee Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NegHODAprv" SortExpression="NegHODAprv" HeaderText="Approved">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ApprovalReason" SortExpression="ApprovalReason" HeaderText="Approval Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upMissingInOut" runat="server">
                            <%--MissingInOut section--%>
                            <ContentTemplate>
                                <%-- ****************************** MissingInOut UnApproved records section  *****************************--%>
                                <div class="panel" id="div_MIOUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Missing In / Out Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--Paste Grid code here-->
                                        <asp:GridView ID="gvMissingInOutUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvMissingInOutUnApproved_PageIndexChanging"
                                            OnRowDataBound="gvMissingInOutUnApproved_RowDataBound" OnSorting="gvMissingInOutUnApproved_Sorting"
                                            OnRowCommand="gvMissingInOutUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOAttSubmit2HOD" HeaderText="MIOAttSubmit2HOD">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                                    DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOHODSubmit" SortExpression="MIOHODSubmit" HeaderText="Submit">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOType" SortExpression="MIOType" HeaderText="Negative Type">
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOEmpReason" SortExpression="MIOEmpReason" HeaderText="Employee Reason">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Approve">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAprvMissingInOut" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlAprvMissingInOut_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="40px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlRoleTypeMissingInOut" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlRoleTypeMissingInOut_SelectedIndexChanged" AutoPostBack="true"
                                                            Width="100px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkMissingInOut" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnMissingInOutChecked" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheckMIO">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMissingInOut" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditMissingInOut" runat="server" ForeColor="#004999" OnClick="btnEditMissingInOut_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="ReSubmit">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReSubmitMissingInOut" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                            ImageUrl="~/images/icon_go.gif" OnClick="btnReSubmitMissingInOut_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>


                                                <asp:BoundField DataField="balCasual" SortExpression="balCasual" HeaderText="C-L">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                    <div class="panel_footer">
                                        <p>
                                            Total :
                                            <asp:Label ID="lblMissingInOutTotal" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Submission :
                                            <asp:Label ID="lblMissingInOutNotSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Submitted :
                                            <asp:Label ID="lblMissingInOutSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Approved :
                                            <asp:Label ID="lblMissingInOutApproved" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Approval :
                                            <asp:Label ID="lblMissingInOutNotApproved" runat="server"></asp:Label>
                                        </p>
                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnMissingInOutSave" class="round_corner btn" Text="Save"
                                                OnClick="btnMissingInOutSave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->
                                </div>
                                <!--end panel-->
                                <%-- ****************************** MissingInOut Approved records section  *****************************--%>
                                <div class="panel approved" id="div_MIOApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Missing In / Out [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--paste grid code here-->
                                        <asp:GridView ID="gvMissingInOutApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvMissingInOutApproved_PageIndexChanging"
                                            OnSorting="gvMissingInOutApproved_Sorting" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                                    DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                                    <ItemStyle Width="30px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOHODSubmit" SortExpression="MIOHODSubmit" HeaderText="Submit">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOEmpReason" SortExpression="MIOEmpReason" HeaderText="Employee Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOHODAprv" SortExpression="MIOHODAprv" HeaderText="Approved">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="MIOApprovalReason" SortExpression="MIOApprovalReason"
                                                    HeaderText="Approval Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upLeaves" runat="server">
                            <%--leaves section--%>
                            <ContentTemplate>
                                <%-- ****************************** Leaves UnApproved records section  *****************************--%>
                                <div class="panel" id="div_leavesUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Leaves For Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <asp:GridView ID="gvLeavesUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvLeavesUnApproved_PageIndexChanging"
                                            OnRowDataBound="gvLeavesUnApproved_RowDataBound" OnSorting="gvLeavesUnApproved_Sorting"
                                            OnRowCommand="gvLeavesUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Submit2HOD" HeaderText="Submit2HOD">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvType_Id" SortExpression="EmpLvType_Id" HeaderText="EmpLvType_Id">
                                                    <ItemStyle Width="100px" CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveGroup" SortExpression="LeaveGroup" HeaderText="LeaveGroup">
                                                    <ItemStyle Width="100px" CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="employeecode" SortExpression="employeecode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="balCasual" SortExpression="balCasual" HeaderText="C-L"></asp:BoundField>
                                                <asp:BoundField DataField="balAnnual" SortExpression="balAnnual" HeaderText="A-L"></asp:BoundField>
                                                <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                                    <ItemStyle Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="HOD Aprv">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlApprvLeave" OnSelectedIndexChanged="ddlApprvLeave_SelectedIndexChanged"
                                                            CssClass="dropdownlist" runat="server" AutoPostBack="true" Width="45px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="45px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlLeaveType" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="100px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkLeaves" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnLeavesCheck" runat="server" CausesValidation="False" CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkLeaves" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditLeaves" runat="server" ForeColor="#004999" OnClick="btnEditLeaves_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="ReSubmit">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnLeavesResubmit" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                            ImageUrl="~/images/icon_go.gif" OnClick="btnLeavesResubmit_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                    <div class="panel_footer">
                                        <p>
                                            Total :
                                            <asp:Label ID="lblLeavesTotal" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Submission :
                                            <asp:Label ID="lblLeavesNotSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Submitted :
                                            <asp:Label ID="lblLeavesSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Approved :
                                            <asp:Label ID="lblLeavesApproved" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Approval :
                                            <asp:Label ID="lblLeavesNotApproved" runat="server"></asp:Label>
                                        </p>
                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnLeavesSave" class="round_corner btn" Text="Save"
                                                OnClick="btnLeavesSave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->
                                </div>
                                <!--end panel-->
                                <%-- ****************************** Leaves Approved records section  *****************************--%>
                                <div class="panel approved" id="div_leavesApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Leaves [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <asp:GridView ID="gvLeavesApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvLeavesApproved_PageIndexChanging"
                                            OnSorting="gvLeavesApproved_Sorting" AllowPaging="True" AllowSorting="True" PageSize="1000"
                                            EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmployeeLeaveType" SortExpression="AttDate" HeaderText="Leave Type">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODleaveAPV" SortExpression="HODleaveAPV" HeaderText="HOD Apv">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODLvRemarks" SortExpression="HODLvRemarks" HeaderText="HOD Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upHalfDay" runat="server">
                            <%--half days section--%>
                            <ContentTemplate>
                                <%-- ****************************** Half Day UnApproved records section  *****************************--%>
                                <div class="panel" id="div_halfDayUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Half Day Leaves Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <asp:GridView ID="gvHalfDayUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvHalfDayUnApproved_PageIndexChanging"
                                            OnRowDataBound="gvHalfDayUnApproved_RowDataBound" OnSorting="gvHalfDayUnApproved_Sorting"
                                            OnRowCommand="gvHalfDayUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Submit2HOD" HeaderText="Submit2HOD">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvType_Id" SortExpression="EmpLvType_Id" HeaderText="EmpLvType_Id">
                                                    <ItemStyle Width="100px" CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveGroup" SortExpression="LeaveGroup" HeaderText="LeaveGroup">
                                                    <ItemStyle Width="100px" CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="employeecode" SortExpression="employeecode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="balCasual" SortExpression="balCasual" HeaderText="C-L"></asp:BoundField>
                                                <asp:BoundField DataField="balAnnual" SortExpression="balAnnual" HeaderText="A-L"></asp:BoundField>
                                                <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                                    <ItemStyle Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="HOD Aprv">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAprvHalfDay" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlAprvHalfDay_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="45px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="45px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlHalfDayType" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlHalfDayType_SelectedIndexChanged"
                                                            AutoPostBack="true" Width="100px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkHalfDay" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnHalfDayCheck" runat="server" CausesValidation="False" CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkHalfDay" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditHalfDay" runat="server" ForeColor="#004999" OnClick="btnEditHalfDay_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="ReSubmit">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReSubmitHalfDay" runat="server" CommandArgument='<%# Eval("Att_Id") %>'
                                                            ImageUrl="~/images/icon_go.gif" OnClick="btnReSubmitHalfDay_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                    <div class="panel_footer">
                                        <p>
                                            Total :
                                            <asp:Label ID="lblHalfDaysTotal" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Submission :
                                            <asp:Label ID="lblHalfDayNotSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Submitted :
                                            <asp:Label ID="lblHalfDaySubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Approved :
                                            <asp:Label ID="lblHalfDayApproved" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Approval :
                                            <asp:Label ID="lblHalfDayNotApproved" runat="server"></asp:Label>
                                        </p>
                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnHalfDaySave" class="round_corner btn" Text="Save"
                                                OnClick="btnHalfDaySave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->
                                </div>
                                <!--end panel-->
                                <%-- ****************************** Half Day Approved records section  *****************************--%>
                                <div class="panel approved" id="div_halfDayApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Half Day Leaves [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <asp:GridView ID="gvHalfDayApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvHalfDayApproved_PageIndexChanging"
                                            OnSorting="gvHalfDayApproved_Sorting" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="Att_Id" HeaderText="id">
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
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmployeeLeaveType" SortExpression="AttDate" HeaderText="Leave Type">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODleaveAPV" SortExpression="HODleaveAPV" HeaderText="HOD Apv">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODLvRemarks" SortExpression="HODLvRemarks" HeaderText="HOD Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                </div>
                                <!--end panel-->
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="upReservations" runat="server">
                            <%--Reservation section--%>
                            <ContentTemplate>
                                <%-- ****************************** Reservation UnApproved records section  *****************************--%>
                                <div class="panel" id="div_ReservationsUnApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Reservation Leave Approval
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--Paste Grid code here-->
                                        <asp:GridView ID="gvReservationUnApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvReservationUnApproved_PageIndexChanging"
                                            OnRowDataBound="gvReservationUnApproved_RowDataBound" OnSorting="gvReservationUnApproved_Sorting"
                                            OnRowCommand="gvReservationUnApproved_RowCommand" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:BoundField DataField="EmpLeave_Id" HeaderText="id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveType_Id" SortExpression="LeaveType_Id" HeaderText="LeaveType_Id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ERPProcess" SortExpression="ERPProcess" HeaderText="LeaveType_Id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="7px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="employeecode" SortExpression="employeecode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveFrom" HeaderText="From Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveTo" HeaderText="To Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                                    HtmlEncode="False">
                                                    <ItemStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveDays" HeaderText="Days">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveType" HeaderText="Leave Type">
                                                    <ItemStyle Width="110px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveReason" SortExpression="LeaveReason" HeaderText="Reason">
                                                    <ItemStyle Width="180px" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="HOD Aprv">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAprvReservation" CssClass="dropdownlist" runat="server"
                                                            OnSelectedIndexChanged="ddlAprvReservation_SelectedIndexChanged" AutoPostBack="true" Width="60px">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Aprv Category">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAprvCategory" runat="server" AutoPostBack="true" Width="60px" CssClass="dropdownlist" OnSelectedIndexChanged="ddlAprvCategory_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HOD Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReason" placeholder="Type Here..." runat="server" CssClass="textbox"
                                                            MaxLength="250" Width="98%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="300px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="cb">
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkReservations" runat="server" />
                                                    </EditItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbtnReservationCheck" runat="server" CausesValidation="False"
                                                            CommandName="toggleCheck">Check</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkReservations" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Copy">
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditReservation" runat="server" ForeColor="#004999" OnClick="btnEditReservation_Click"
                                                            Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                            CommandArgument='<%# Eval("EmpLeave_Id") %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ReSubmit">
                                                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReservationReSubmit" runat="server" CommandArgument='<%# Eval("EmpLeave_Id") %>'
                                                            ImageUrl="~/images/icon_go.gif" OnClick="btnReservationReSubmit_Click" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <!--end panel_body-->
                                    <div class="panel_footer">
                                        <p>
                                            Total :
                                            <asp:Label ID="lblReservationTotal" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Submission :
                                            <asp:Label ID="lblReservationNotSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Submitted :
                                            <asp:Label ID="lblReservationSubmitted" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Approved :
                                            <asp:Label ID="lblReservationApproved" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Pending Approval :
                                            <asp:Label ID="lblReservationNotApproved" runat="server"></asp:Label>
                                        </p>
                                        <div class="save_btn">
                                            <asp:Button runat="server" ID="btnReservationSave" class="round_corner btn" Text="Save"
                                                OnClick="btnReservationSave_Click" />
                                        </div>
                                    </div>
                                    <!--end panel_footer-->
                                </div>
                                <!--end panel-->
                                <%-- ****************************** Reservation Approved records section  *****************************--%>
                                <div class="panel approved" id="div_reservationsApproved" runat="server">
                                    <div class="panel_head">
                                        <p>
                                            Reservation [Approved]
                                        </p>
                                    </div>
                                    <!--end panel_head-->
                                    <div class="panel_body">
                                        <!--paste grid code here-->
                                        <asp:GridView ID="gvReservationApproved" SkinID="GridView" runat="server" Width="100%"
                                            AutoGenerateColumns="False" HorizontalAlign="Center" OnPageIndexChanging="gvReservationApproved_PageIndexChanging"
                                            OnSorting="gvReservationApproved_Sorting" AllowPaging="True" AllowSorting="True"
                                            PageSize="1000" EmptyDataText="No Data Exist!">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="8px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="employeecode" SortExpression="employeecode" HeaderText="Code">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FullName" SortExpression="FullName" HeaderText="Name">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveFrom" SortExpression="LeaveFrom" HeaderText="From Date"
                                                    DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveTo" SortExpression="LeaveTo" HeaderText="To Date"
                                                    DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                                    <ItemStyle Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveDays" SortExpression="LeaveDays" HeaderText="Days">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveType" SortExpression="LeaveType" HeaderText="Leave Type">
                                                    <ItemStyle Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LeaveReason" SortExpression="LeaveReason" HeaderText="Reason">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODApproval" SortExpression="HODApproval" HeaderText="HOD Approval">
                                                    <ItemStyle Width="10px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="HODRemakrs" SortExpression="HODRemakrs" HeaderText="HOD Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BODApproval" SortExpression="BODApproval" HeaderText="BOD Approval">
                                                    <ItemStyle />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BODRemarks" SortExpression="BODRemarks" HeaderText="BOD Remarks">
                                                    <ItemStyle Width="200px" />
                                                </asp:BoundField>
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