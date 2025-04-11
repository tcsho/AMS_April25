<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Resignation.aspx.cs" Inherits="Resignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
<%--    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <style type="text/css">
        .controlses {
            margin-top: -30px;
        }
            .LabelWhite {
        font-size: 18px;
        /*color: #FFF;*/
        color:#ffff00;
 
        margin-bottom: 5px;
        margin-left:2em;
    }
            .style-changes td{
                vertical-align: top !important;
            }
            .style-changes textarea{
                resize: vertical;
            }
    </style>
    <script type="text/javascript"> 

        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            /*Date Picker*/
            $(function () { 
                $('.datepicker').datepicker({
                    dateFormat: "dd/mm/yy",
                    minDate: -7
                });
                $("#anim").change(function () {
                    $(".datepicker").datepicker("option", "showAnim", $(this).val());
                });
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
            <div class="outer_wrap">
                
                <div class="body_content fullrow">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="upLateArrivals" runat="server">
                            <%--Late Arrivals records--%>
                            <ContentTemplate>

                                <div class="panel">
                            <div style="margin: 0 auto;">
                               <div style="width: 20%; color: white; text-align:end; font-size:25px;">Resignation Request</div>
                                <br />
                               <%-- <div style="color: white; font-size: large; font-family: cursive;">Total leaves for year 2023 : 45</div>--%>
                                <table class="style-changes" style="width: 1000px;" cellspacing="0" cellpadding="0" align="center">
                                    <tr id="lvdata" runat="server">
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td>
                                            <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr id="regDepartmentEmployee">
                                                    <td style="width: 20%; color: rgba(255,255,0,1);"> <asp:Label ID="txtRegDepartment" runat="server" style="color: rgba(255,255,0,1);">Select Department *:</asp:Label>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="regDepartment" Visible="false" runat="server" Width="275px" AutoPostBack="True" OnSelectedIndexChanged="regDepartment_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 20%; color: rgba(255,255,0,1);"> <asp:Label ID="txtRegEmployee" runat="server" style="color: rgba(255,255,0,1);">Select Employee *:</asp:Label>
                                                    </td>
                                                    <td>
                                                         <asp:DropDownList ID="regEmployee" Visible="false" runat="server" Width="275px" AutoPostBack="True" OnSelectedIndexChanged="regEmployee_SelectedIndexChanged">
                                                         <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>   
                                                         </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr style="height: 10px">
                                                    <td colspan="4"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; color: rgba(255,255,0,1);">Resignation date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="submissionDate" runat="server" Width="275px" CssClass="datepicker"
                                                            AutoPostBack="True" OnTextChanged="resignationSubmDate_TextChanged" DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>&nbsp;
                                                        
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="submissionDate"
                                                            Display="Dynamic" ErrorMessage="*Select Resignation Submission Date" InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="policyText" runat="server" style="color: rgba(255,255,0,1);"></asp:Label>
                                                    </td>
                                                     <td>
                                                        <asp:TextBox ID="policyDate" Width="275px" Visible="false" runat="server" Enabled="false" DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; color: rgba(255,255,0,1);">Last working date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="lastDayDate" Enabled="true" runat="server" Width="275px" CssClass="datepicker" AutoPostBack="True"
                                                            OnTextChanged="lasDayDate_TextChanged" DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>&nbsp; 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastDayDate"
                                                            Display="Dynamic" ErrorMessage="*Select To Date " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                     <td style="width: 20%; color: rgba(255,255,0,1);">Notice period:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="noticeDays" runat="server" Width="275px" MaxLength="100" CssClass="textbox"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    
                                                </tr>
                                                <tr style="height: 10px">
                                                    <td colspan="4"></td>
                                                </tr>
                                                <tr>
                                                   <td style="width: 20%; color: rgba(255,255,0,1);">Please enter remarks in case of change in last working date:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="lastDateRemarks" TextMode="MultiLine" runat="server" Rows="3" Width="275px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 20%; color: rgba(255,255,0,1);">Reason of resignation *:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlreason" runat="server" Width="275px" Visible="true"
                                                            AutoPostBack="True" CssClass="dropdownlist">
                                                            <asp:ListItem Enabled="true" Text="Select Reason" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Deceased" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Gross misconduct" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Retirement" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Resigned" Value="4"></asp:ListItem>
                                                            <%--<asp:ListItem Text="Terminated" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Terminated on performance" Value="6"></asp:ListItem>--%>
                                                            <asp:ListItem Text="Contract ended" Value="5"></asp:ListItem>
                                                            <%--<asp:ListItem Text="Deceased" Value="8"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlreason"
                                                            Display="Dynamic" ErrorMessage="*Select Reservation Type " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; color: rgba(255,255,0,1);" valign="top">Comments by employee *:
                                                    </td>
                                                    <td align="left" colspan="3">
                                                        <asp:TextBox ID="employeeComments" runat="server" Width="275px" Height="50px" MaxLength="500"
                                                            Rows="15" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="employeeComments"
                                                            Display="Dynamic" ErrorMessage="*Reservation Reason Required !" InitialValue="0"
                                                            ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height: 10px">
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr id="lvbtn" runat="server">
                                        <td style="width: 5%"></td>
                                        <td align="center">
                                            <asp:Label ID="lbl_res_error" runat="server" Text=""></asp:Label>
                                            <br />
                                            <asp:Button ID="btnSubmitResignation" runat="server" Text="Save" ValidationGroup="S"
                                                CssClass="button" OnClick="btnSubmitResignation_Click" OnClientClick="return ConfirmAttachment()" />
                                            &nbsp; &nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="button" OnClick="btnReset_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                             
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </div>
                    <!--end inner_wrap-->
                    <div class="panel" id="div_leaveRequests" runat="server">
                            <div class="panel_head">
                                <p>
                                    Resignation Status

                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvResignation" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" AllowPaging="True" AllowSorting="True"
                                    PageSize="1000" EmptyDataText="No Data Exist!">
                                    <Columns>
                                            <asp:BoundField DataField="EmployeeCodeAndName" SortExpression="Status" HeaderText="Employee Code And Name">
                                            <ItemStyle Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SubmissionDate" SortExpression="SubmissionDate" HeaderText="Submission Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastWorkingDate" SortExpression="LastWorkingDate" HeaderText="Last Working Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" SortExpression="Reason" HeaderText="Reason">
                                            <ItemStyle Width="180px" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="NoticePeriod" SortExpression="NoticePeriod" HeaderText="NoticePeriod">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" SortExpression="Status" HeaderText="Status">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_footer-->
                        </div>
                </div>
                <!--end body_content-->
            </div>
            <!--end outer_wrap-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
