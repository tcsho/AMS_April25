<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeLeavesSubmissions.aspx.cs" Inherits="EmployeeLeavesSubmissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });
            $('.leave_balance .footer').click(function () {
                $('.leave_balance .header').slideToggle("slow");
                $('.leave_icon').toggleClass('leave_icon_hide');
            });


            /*Date Picker*/
            $(function () {
                $(".datepicker").datepicker();
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

    <style type="text/css">
        .controlses {
            margin-top: -30px;
        }

        .LabelWhite {
            font-size: 18px;
            /*color: #FFF;*/
            color: #ffff00;
            margin-bottom: 5px;
            margin-left: 2em;
        }

        /* popup_box DIV-Styles*/
        #popup_box {
            display: none; /* Hide the DIV */
            position: fixed;
            _position: absolute; /* hack for internet explorer 6 */
            height: 420px;
            width: 530px;
            background: #FFFFFF;
            /*left: 300px;*/
            margin: 0 auto;
            top: 150px;
            z-index: 100; /* Layering ( on-top of others), if you have lots of layers: I just maximized, you can change it yourself */
            margin-left: 15px;
            /* additional features, can be omitted */
            border: 2px solid #ff0000;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px #ff0000;
            -webkit-box-shadow: 0 0 5px #ff0000;
            box-shadow: 0 0 5px #ff0000;
        }

        a {
            cursor: pointer;
            text-decoration: none;
        }

        /* This is for the positioning of the Close Link */
        #popupBoxClose {
            font-size: 20px;
            line-height: 15px;
            right: 5px;
            top: 5px;
            position: absolute;
            color: #6fa5e2;
            font-weight: 500;
        }

        /* Basic reset */
        body, button {
            margin: 0;
            padding: 0;
            border: 0;
            box-sizing: border-box;
        }

        /* Ensure the container takes the full width of its parent */
        .button-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            padding: 10px;
        }

        /* Style the buttons */
        .responsive-button {
            background: linear-gradient(#CCC,rgba(102,102,102,1));
            cursor: pointer;
            color: #FFF;
            font-weight: bold;
            border: #FFF solid 2px;
            height: 35px;
            flex: 1 1 12%; /* Grow and shrink, with a base width of 12% */
            margin: 5px;
            padding: 10px;
            margin-left: 5px;
            border-radius: 4px;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }

            /* Change color on hover */
            .responsive-button:hover {
                background: linear-gradient(#CCC,#FFF);
                color: #09F;
            }

        .label {
            flex: 1 1 auto; /* Allows the label to take up space but be flexible */
            text-align: center;
            margin-right: 10px;
            font-size: 1.2em;
            float: right;
        }

        .dropdown {
            border: 1px solid #ccc;
            border-radius: 4px;
            margin-top: 5px;
        }

        @media (max-width: 768px) {
            .container {
                flex-direction: column;
                align-items: flex-start;
            }

            .btn, .dropdown {
                width: 100%;
                margin-bottom: 10px;
            }

            .label {
                margin: 0;
            }
        }
        /* Adjust for smaller screens */
        @media (max-width: 600px) {
            .responsive-button {
                flex: 1 1 48%; /* Adjust to fit two buttons per row on small screens */
            }
        }

        @media (max-width: 400px) {
            .responsive-button {
                flex: 1 1 100%; /* Stack buttons vertically on very small screens */
            }
        }

        .popup {
            display: none; /* Hidden by default */
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5); /* Black background with transparency */
            z-index: 1000; /* Sit on top */
        }

        .popup-content {
            background-color: #fefefe;
            margin: 15% auto; /* 15% from the top and centered */
            padding: 20px;
            border: 1px solid #888;
            width: 300px; /* Could be more or less, depending on screen size */
        }

        .close-btn {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close-btn:hover,
            .close-btn:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }
    </style>

    <div style="width: 580px; height: 100%; margin: 0 auto;">
        <div style="display: table; height: 100%; position: relative; overflow: hidden;">
            <div style="position: absolute; top: 50%; display: table-cell; vertical-align: middle;">
                <div style="position: relative; top: -50%">
                    <div id="popup_box">
                        <!-- OUR PopupBox DIV-->
                        <img src="images/b-day.jpg" width="530" height="398" alt="Happy Birthday" />
                        <a id="popupBoxClose">Close</a>
                    </div>
                </div>
            </div>
        </div>
    </div> 

    <div id="popup_boxPwExp" class="popup">
        <div class="popup-content">
            <span class="close-btn" id="popupBoxPwExpClose">&times;</span>
            <h1 style="color:red;">Password Expiry Notification!</h1><br/>
            <p>Your password has expired.<br/> Please <a href="/ChPass.aspx ">Change Password</a></p>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <div class="leave_balance">
                            <div class="header">
                                <div class="one_of_two">
                                    <p>
                                        Available Casual Leaves :
                                        <asp:Label ID="lblActCas" runat="server"></asp:Label>
                                    </p>
                                    <p>
                                        Pending Approval :
                                        <asp:Label ID="lblNonApvCas" runat="server"></asp:Label>
                                    </p>
                                    <p>
                                        Balance of Casual Leaves :
                                        <asp:Label ID="lblCas" runat="server"></asp:Label>
                                    </p>
                                </div>
                                <div class="one_of_two">
                                    <p>
                                        Available Annual Leaves :
                                        <asp:Label ID="lblActAnu" runat="server"></asp:Label>
                                    </p>
                                    <p>
                                        Pending Approval :
                                        <asp:Label ID="lblNonApvAnu" runat="server"></asp:Label>
                                    </p>
                                    <p>
                                        Balance Annual Leaves :
                                        <asp:Label ID="lblAnu" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="footer">
                                <p class="leave_icon">
                                    click to Down
                                </p>
                            </div>
                            <div class="label">
                                <asp:Label ID="lblTimeIn" CssClass="LabelWhite" Text="Today's Time In: -" runat="server" />
                            </div>
                        </div>
                        <!--end leave_balance-->
                    </div>
                    <!--end inner wrap-->
                </div>
                <%-- ****************************** drop down section  *****************************--%>

                <div class="button-container">
                    <input type="button" class="show_hide_btn responsive-button" value="Show/Hide Approved" />
                    <%--<button class="responsive-button">Button 2</button>--%>
                    <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdown" Width="85px" AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="btnAttReport" runat="server" class="responsive-button" Text="Attendance Report" OnClick="btnViewAttReport_Click" />
                    <asp:Button ID="btnLogReport" runat="server" class="responsive-button" Text="Employee Log" OnClick="btnViewLogReport_Click" />
                    <asp:Button ID="btnUpdReport" runat="server" class="responsive-button" Text="Update Attendance" OnClick="btnUpdateAtt_Click" />
                    <asp:Button runat="server" ID="btnAddReservation" class="responsive-button" Text="Leave Request" OnClick="lnkAdd_Click" />
                    <asp:Button runat="server" ID="btnAddReservationOFT" class="responsive-button" Text="Official Tour/Task" OnClick="lnkAddOFT_Click" />
                </div>
                <div id="TrEMP" runat="server" style="margin-left: 310px;">
                    <h2 style="color: White">Click <a style="font-size: larger" href="Files/AMS_Employee.pdf">here</a> to download AMS user manual for Employee.</h2>
                </div>

                <!--end fullrow-->
                <div class="body_content fullrow">
                    <div class="inner_wrap">
                        <%-- ****************************** LateArrivals UnApproved records section  *****************************--%>
                        <div class="panel" id="div_lteNotSubmitted" runat="server">
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
                                    Late Arrival Submission
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvLateArrivals" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvLateArrivals_PageIndexChanging"
                                    OnRowDataBound="gvLateArrivals_RowDataBound" OnSorting="gvLateArrivals_Sorting"
                                    OnRowCommand="gvLateArrivals_RowCommand" AllowPaging="True" AllowSorting="True"
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Attendance Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAttendanceTypeLateArrival" CssClass="dropdownlist" runat="server"
                                                    AutoPostBack="true" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" MaxLength="250" Width="98%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="400px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="cb">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Copy">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnCopyLateArrivals" runat="server" ForeColor="#004999" OnClick="btnCopyLateArrivals_Click"
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
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <p>
                                    Total :
                                    <asp:Label ID="lblLateArrivalsTotal" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Not Submitted :
                                    <asp:Label ID="lblLateArrivalsNotSubmitted" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Submitted :
                                    <asp:Label ID="lblLateArrivalsSubmitted" runat="server"></asp:Label>
                                </p>
                                <div class="save_btn">
                                    <asp:Button runat="server" ID="btnLateArrivalsSubmit" class="round_corner btn" Text="Save"
                                        OnClick="btnLateArrivalsSubmit_Click" />
                                </div>
                            </div>
                            <!--end panel_footer-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** LateArrivals Approved records section  *****************************--%>
                        <div class="panel approved" id="div_lteSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Late Arrivals [Submitted]
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--paste grid code here-->
                                <asp:GridView ID="gvLateArrivalsSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvLateArrivalsSubmitted_PageIndexChanging"
                                    OnSorting="gvLateArrivalsSubmitted_Sorting" AllowPaging="True" AllowSorting="True"
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AttendanceType" SortExpression="AttendanceType" HeaderText="Attendance Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegEmpReason" SortExpression="NegEmpReason" HeaderText="Employee Reason">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApprovalReason" SortExpression="ApprovalReason" HeaderText="HOD Apv">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ApprovalReason" SortExpression="ApprovalReason" HeaderText="HOD Remarks">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label1" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** MissingInOut UnApproved records section  *****************************--%>
                        <div class="panel" id="div_missingNotSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Missing In / Out Submission
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvMIO" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvMIO_PageIndexChanging" OnRowDataBound="gvMIO_RowDataBound"
                                    OnSorting="gvMIO_Sorting" OnRowCommand="gvMIO_RowCommand" AllowPaging="True"
                                    AllowSorting="True" PageSize="1000" EmptyDataText="No Data Exist!">
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Attendance Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAttendanceTypeMissingInOut" CssClass="dropdownlist" runat="server"
                                                    AutoPostBack="true" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" MaxLength="250" Width="98%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="400px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="cb">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Copy">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnCopyMIO" runat="server" ForeColor="#004999" OnClick="btnCopyMIO_Click"
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
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label2" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <p>
                                    Total :
                                    <asp:Label ID="lblMissingInOutTotal" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Not Submitted :
                                    <asp:Label ID="lblMissingInOutnotSubmitted" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Submitted :
                                    <asp:Label ID="lblMissingInOutSubmitted" runat="server"></asp:Label>
                                </p>
                                <div class="save_btn">
                                    <asp:Button runat="server" ID="btnMissingInOutSubmit" class="round_corner btn" Text="Save"
                                        OnClick="btnMissingInOutSubmit_Click" EnableViewState="true" />
                                </div>
                            </div>
                            <!--end panel_footer-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** MissingInOut Approved records section  *****************************--%>
                        <div class="panel approved" id="div_missingSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Missing In / Out [Submitted]
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--paste grid code here-->
                                <asp:GridView ID="gvMIOSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvMIOSubmitted_PageIndexChanging"
                                    OnSorting="gvMIOSubmitted_Sorting" AllowPaging="True" AllowSorting="True" PageSize="1000"
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NegType" SortExpression="NegType" HeaderText="Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AttendanceType" SortExpression="AttendanceType" HeaderText="Attendance Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOEmpReason" SortExpression="MIOEmpReason" HeaderText="Employee Reason">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOHODAprv" SortExpression="MIOHODAprv" HeaderText="HOD Apv">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MIOApprovalReason" SortExpression="MIOApprovalReason"
                                            HeaderText="HOD Remarks">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label3" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** Leaves UnApproved records section  *****************************--%>
                        <div class="panel" id="div_leavesNotSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Leaves Submission

                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <asp:GridView ID="gvLeavesNotSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvLeavesNotSubmitted_PageIndexChanging"
                                    OnRowDataBound="gvLeavesNotSubmitted_RowDataBound" OnSorting="gvLeavesNotSubmitted_Sorting"
                                    OnRowCommand="gvLeavesNotSubmitted_RowCommand" AllowPaging="True" AllowSorting="True"
                                    PageSize="1000" EmptyDataText="No Data Exist!">
                                    <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="T" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Leave Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlRoleType" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlRoleType_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Attendance Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAttendanceType" CssClass="dropdownlist" runat="server" OnSelectedIndexChanged="ddlAttendanceType_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" MaxLength="250" Width="98%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="350px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="cb">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Copy">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnCopyleaves" runat="server" ForeColor="#004999" OnClick="btnCopyleaves_Click"
                                                    Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                    CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpLvType_Id" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <%-- <asp:BoundField DataField="AttTypeId" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="EmpLvReason" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="R" HeaderText="R">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="G" HeaderText="G">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B" HeaderText="B">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label4" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <p>
                                    Total :
                                    <asp:Label ID="lblLeavesTotal" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Not Submitted :
                                    <asp:Label ID="lblLeavesNotSubmitted" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Submitted :
                                    <asp:Label ID="lblLeavesSubmitted" runat="server"></asp:Label>
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
                        <div class="panel approved" id="div_leavesSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Leaves [Submitted]
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <asp:GridView ID="gvLeavesSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvLeavesSubmitted_PageIndexChanging"
                                    OnSorting="gvLeavesSubmitted_Sorting" AllowPaging="True" AllowSorting="True"
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Leave Reason">
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
                                <asp:Label ID="Label5" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** Half Day UnApproved records section  *****************************--%>
                        <div class="panel" id="div_halfDayNotSubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Half Day Leaves Submission

                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <asp:GridView ID="gvHalgDayNonSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvHalgDayNonSubmitted_PageIndexChanging"
                                    OnRowDataBound="gvHalgDayNonSubmitted_RowDataBound" OnSorting="gvHalgDayNonSubmitted_Sorting"
                                    OnRowCommand="gvHalgDayNonSubmitted_RowCommand" AllowPaging="True" AllowSorting="True"
                                    PageSize="40" EmptyDataText="No Data Exist!">
                                    <Columns>
                                        <asp:BoundField DataField="Att_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="T" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeIn" SortExpression="TimeIn" HeaderText="TimeIn" DataFormatString="{0:HH:mm:ss}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeOut" SortExpression="TimeOut" HeaderText="TimeOut"
                                            DataFormatString="{0:HH:mm:ss}" HtmlEncode="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Leave Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlRoleTypeHalfDays" CssClass="dropdownlist" runat="server"
                                                    OnSelectedIndexChanged="ddlRoleTypeHalfDays_SelectedIndexChanged" AutoPostBack="true"
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Attendance Type">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAttendanceTypeHalfDays" CssClass="dropdownlist" runat="server" 
                                                    OnSelectedIndexChanged="ddlAttendanceTypeHalfDays_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="textbox" MaxLength="250" Width="98%"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="350px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="cb">
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </EditItemTemplate>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Copy">
                                            <ItemStyle Wrap="False" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnCopyhalfDays" runat="server" ForeColor="#004999" OnClick="btnCopyhalfDays_Click"
                                                    Style="text-align: center; font-weight: bold;" ToolTip="Copy" ImageUrl="~/images/edit.gif"
                                                    CommandArgument='<%# Eval("Att_Id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmpLvType_Id" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="AttTypeId" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="EmpLvReason" HeaderText="T">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ERPProcessLock" HeaderText="ERPProcessLock">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="R" HeaderText="R">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="G" HeaderText="G">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="B" HeaderText="B">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label6" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <p>
                                    Total :
                                    <asp:Label ID="lblHalfDaysTotal" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Not Submitted :
                                    <asp:Label ID="lblHalfDayNotSubmitted" runat="server"></asp:Label>
                                </p>
                                <p>
                                    Submitted :
                                    <asp:Label ID="lblHalfDaySubmitted" runat="server"></asp:Label>
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
                        <div class="panel approved" id="div_halfDaySubmitted" runat="server">
                            <div class="panel_head">
                                <p>
                                    Half Day Leaves [Submitted]
                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <asp:GridView ID="gvHalfDaysSubmitted" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvHalfDaysSubmitted_PageIndexChanging"
                                    OnSorting="gvHalfDaysSubmitted_Sorting" AllowPaging="True" AllowSorting="True"
                                    PageSize="40" EmptyDataText="No Data Exist!">
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
                                        <asp:BoundField DataField="AttDate" SortExpression="AttDate" HeaderText="Date" DataFormatString="{0:dddd dd/MM/yyyy}"
                                            HtmlEncode="False">
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmployeeLeaveType" HeaderText="Leave Type">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="AttendanceType" HeaderText="Attendance Type">
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="EmpLvReason" SortExpression="EmpLvReason" HeaderText="Leave Reason">
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
                                <asp:Label ID="Label7" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                        </div>
                        <!--end panel-->
                        <%-- ****************************** Reservation UnApproved records section  *****************************--%>
                        <div class="panel" id="div_leaveRequests" runat="server">
                            <div class="panel_head">
                                <p>
                                    Leave Requests

                                </p>
                            </div>
                            <!--end panel_head-->
                            <div class="panel_body">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvReservations" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvReservations_PageIndexChanging"
                                    OnRowDataBound="gvReservations_RowDataBound" OnRowDeleting="gvReservations_RowDeleting"
                                    OnSorting="gvReservations_Sorting" OnSelectedIndexChanging="gvReservations_SelectedIndexChanging"
                                    AllowPaging="True" AllowSorting="True" EmptyDataText="No Data Exist!">
                                    <Columns>
                                        <asp:BoundField DataField="EmpLeave_Id" HeaderText="id">
                                            <ItemStyle CssClass="hide" Width="10px"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODApprove" HeaderText="HODApprove">
                                            <ItemStyle CssClass="hide" Width="10px"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LeaveType" SortExpression="LeaveType" HeaderText="Reservation Type">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveFrom" SortExpression="LeaveFrom" HeaderText="From Date"
                                            DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveTo" SortExpression="LeaveTo" HeaderText="To Date"
                                            DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveDays" SortExpression="LeaveDays" HeaderText="Days">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LeaveReason" SortExpression="LeaveReason" HeaderText="Reason">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODAPV" SortExpression="HODAPV" HeaderText="HOD Approval">
                                            <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HODRemakrs" SortExpression="HODRemakrs" HeaderText="HOD Remarks">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AprvCategory" SortExpression="AprvCategory" HeaderText="Leave Status">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="BODAPV" SortExpression="BODAPV" HeaderText="BOD Approval">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>--%>
                                        <%--<asp:BoundField DataField="BODRemarks" SortExpression="BODRemarks" HeaderText="BOD Remarks">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>--%>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Edit">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    ImageUrl="~/images/edit.gif" Text="Edit" ToolTip="View / Edit" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                    ImageUrl="~/images/delete.gif" Text="" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" />
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Submit2HOD" HeaderText="Submit2HOD">
                                            <ItemStyle CssClass="hide" Width="10px"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="Label8" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <div class="save_btn">
                                    <%--<asp:Button runat="server" ID="btnAddReservation" class="round_corner btn" Text="New Leave Request"
                                        OnClick="lnkAdd_Click" Style="width: 200px;" />--%>
                                </div>
                            </div>
                            <!--end panel_footer-->
                        </div>
                        <!--end panel-->
                        <div class="panel">
                            <div style="margin: 0 auto;">
                                <%-- <div style="color: white; font-size: large; font-family: cursive;">Total leaves for year 2023 : 45</div>--%>
                                <table style="width: 700px;" cellspacing="0" cellpadding="0" align="center">
                                    <tr id="lvdata" runat="server">
                                        <td style="width: 5%">&nbsp;
                                        </td>
                                        <td>
                                            <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <%--<td style="width: 20%">Total leaves for year 2023 : 45 </td>--%>

                                                    <%--  <td align="left">
                                                        <asp:Label ID="totalLeavesPerYear" Text="45" runat="server" Width="140px" 
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <%--<td style="width: 20%">Availed : </td>--%>

                                                    <td align="left">
                                                        <asp:Label ID="availedLeaves" Visible="false" runat="server" Width="140px"
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td style="width: 20%">Balance : </td>--%>

                                                    <td align="left">
                                                        <asp:Label ID="balanceLeaves" Visible="false" runat="server" Width="140px"
                                                            AutoPostBack="True"></asp:Label>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">From Date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFromDate" runat="server" Width="140px" CssClass="datepicker"
                                                            AutoPostBack="True" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>&nbsp;
                                                        (MM/DD/YYYY)
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                            Display="Dynamic" ErrorMessage="*Select From Date" InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">To Date*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="140px" CssClass="datepicker" AutoPostBack="True"
                                                            OnTextChanged="txtToDate_TextChanged"></asp:TextBox>&nbsp;&nbsp;(MM/DD/YYYY)
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToDate"
                                                            Display="Dynamic" ErrorMessage="*Select To Date " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">No. of Days:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtDays" runat="server" Width="50px" MaxLength="100" CssClass="textbox"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%">Reservation Type*:
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlLeaveType" runat="server" Width="142px" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="dropdownlist">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlLeaveType"
                                                            Display="Dynamic" ErrorMessage="*Select Reservation Type " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 3px">
                                                    <td colspan="2"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; vertical-align: middle;">Remarks:
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtReason" runat="server" Width="95%" Height="50px" MaxLength="500"
                                                            Rows="15" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReason"
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
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="S"
                                                CssClass="button" OnClientClick="return ConfirmAttachment()" />
                                            &nbsp;
                                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Cancel" CssClass="button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <!--end inner_wrap-->
                </div>
                <!--end body_content-->
            </div>
            <!--end outer_wrap-->

            <script type="text/javascript">

                var frm_dte_id = '<%=txtFromDate.ClientID%>';
                var to_dte_id = "<%=txtToDate.ClientID%>";

                function ConfirmAttachment() {



                    var f_date = new Date(document.getElementById(frm_dte_id).value);
                    var t_date = new Date(document.getElementById(to_dte_id).value);

                    if (f_date.getDay() == 1 || t_date.getDay() == 6) {

                        //if (confirm("Sundays should be included. Press 'Cancel' to select Sunday or press 'OK' to continue.")) {
                        if (confirm("Please consider sunday attachements while submitting your leaves, Do you want to submit leaves as it is?")) {
                            return true;
                        } else {
                            return false;
                        }

                    } else {
                        return true;
                    }

                }

                // POP-up start 

                $('#popupBoxClose').click(function () {
                    unloadPopupBox();
                });

                function unloadPopupBox() {    // TO Unload the Popupbox
                    $('#popup_box').fadeOut("slow");
                }

                function loadPopupBox() {    // To Load the Popupbox
                    $('#popup_box').fadeIn("slow");
                }

                $('#popupBoxPwExpClose').click(function () {
                    unloadPopupBoxPwExp();

                });

                function unloadPopupBoxPwExp() {    // TO Unload the Popupbox
                    $('#popup_boxPwExp').fadeOut("slow");
                }

                function loadPopupBoxPwExp() {    // To Load the Popupbox
                    $('#popup_boxPwExp').fadeIn("slow");
                }

                var showBD = "<%=show_BDays%>";
                var showPwExpiry = "<%=show_PwExpiry%>";

                if (showBD == "1") {
                    loadPopupBox();
                }

                if (showPwExpiry == "1") {
                    loadPopupBoxPwExp();
                }

    //POP-up end
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>