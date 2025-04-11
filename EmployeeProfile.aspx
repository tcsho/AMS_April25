<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="EmployeeProfile.aspx.cs" Inherits="EmployeeProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables.css">
    <script type="text/javascript" charset="utf8" src="Scripts/jquery.dataTables.min.js">
    </script>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(document).ready(document_Ready);

        function document_Ready() {


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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Employee Profile
                        </p>
                        <div id="divView" runat="server">
                            <div class="controls round_corner">
                                <asp:LinkButton ID="btnAddEmployee" OnClick="btnAddEmployee_Click" runat="server"
                                    CssClass="btn round_corner" CausesValidation="False" Font-Bold="False" Width="150px">Add New Employee</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton1" OnClick="btnAddDesignation_Click" runat="server"
                                    CssClass="btn round_corner" CausesValidation="False" Font-Bold="False" Width="150px">Add New Designation</asp:LinkButton>
                            </div>
                            <div class="panel" runat="server" id="panDesignation" visible="false">
                                <div class="panel_head">
                                    Add a Designation
                                </div>
                                <div class="panel_body">
                                    <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left">*Designation Name :
                                                <asp:TextBox ID="txtDesignation" runat="server" Width="250px" CausesValidation="true"
                                                    MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDesignation"
                                                    Display="Dynamic" ErrorMessage="Designation Name Required" SetFocusOnError="True"
                                                    ForeColor="red" ValidationGroup="d"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Maximum of 100 characters allowed"
                                                    ControlToValidate="txtDesignation" Display="Dynamic" ValidationExpression=".{0,100}"
                                                    ValidationGroup="d" />
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnSaveDesignation" runat="server" Text="Save" CausesValidation="True"
                                                    OnClick="btnSaveDesignation_Click" CssClass="btn round_corner" ValidationGroup="d" />
                                                <asp:Button ID="btnCancelDesig" runat="server" Text="Cancel" CausesValidation="False"
                                                    CssClass="btn round_corner" OnClick="btnCancelDesignation_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label ID="Label1" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                                </div>
                            </div>
                            <div class="panel" runat="server" id="ActiveEmployees">
                                <div class="panel_head">
                                    List of Active Employees
                                </div>
                                <div class="panel_body">
                                    <asp:GridView ID="gv" runat="server" Width="100%" AutoGenerateColumns="False" HorizontalAlign="Center"
                                        EmptyDataText="No Data Exist!" OnPageIndexChanging="gv_PageIndexChanging" OnSorting="gv_Sorting"
                                        AllowPaging="True" AllowSorting="True" PageSize="15">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode"></asp:BoundField>
                                            <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="EmployeeCode">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="EmployeeCode">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RegName" HeaderText="Region Name" SortExpression="Region_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CenName" HeaderText="Center Name" SortExpression="Center_Id">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                            <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesigCode" HeaderText="DesigCode" SortExpression="DesigCode">
                                                <ItemStyle CssClass="hide" />
                                                <HeaderStyle CssClass="hide" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptCode" />
                                            <asp:BoundField DataField="DesigName" HeaderText=" Designation" SortExpression="DesigCode" />
                                            <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" SortExpression="DOJ"
                                                DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="DOB" HeaderText="Date of Birth" SortExpression="DOB" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="MaritalSts" HeaderText="Marital Status" SortExpression="MaritalSts" />
                                            <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                            <asp:BoundField DataField="Inactive" HeaderText="Is Resigned" SortExpression="Inactive"></asp:BoundField>
                                            <asp:BoundField DataField="CasualLeave" HeaderText="Casual Leave" SortExpression="CasualLeave" />
                                            <asp:BoundField DataField="AnnulaLeave" HeaderText="Annual Leave" SortExpression="AnnulaLeave" />
                                            <asp:TemplateField ShowHeader="False" HeaderText="Update Employee Details">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdateEmployeeDetails" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                        CausesValidation="False" CommandName="Edit Profile" OnClick="btnUpdate_Click"
                                                        ImageUrl="~/images/approval.png" Text="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ShowHeader="False" HeaderText="Update Leave Balance">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdateLeaveBalance" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                        CausesValidation="False" CommandName="Edit Leaves" OnClick="btnUpdateLeave_Click"
                                                        ImageUrl="~/images/approval.png" Text="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tr1" />
                                        <HeaderStyle CssClass="tableheader" />
                                        <AlternatingRowStyle CssClass="tr2" />
                                    </asp:GridView>
                                    <asp:Label ID="lab_dataStatus" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divProfileData">
                            <div class="panel">
                                <div class="panel_head">
                                    <p runat="server">
                                        Employee Details
                                    </p>
                                    <div class="new_event">
                                        <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="left">*Employee Code :
                                                    <asp:TextBox ID="txtEmpCode" onkeypress="return isNumberKey(event)" runat="server"
                                                        Width="250px" MaxLength="10" CausesValidation="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmpCode"
                                                        Display="Dynamic" ErrorMessage="Employee Code Required" SetFocusOnError="True"
                                                        ForeColor="red" BackColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*First Name:
                                                    <asp:TextBox ID="txtFName" runat="server" Width="250px" CausesValidation="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFName"
                                                        Display="Dynamic" ErrorMessage="First Name Required" ForeColor="red" BackColor="white"
                                                        SetFocusOnError="True" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Last Name:
                                                    <asp:TextBox ID="txtLName" runat="server" Width="250px" CausesValidation="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLName"
                                                        Display="Dynamic" ErrorMessage="Last Name Required" SetFocusOnError="True" ForeColor="red"
                                                        BackColor="white" ValidationGroup="c"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Full Name:
                                                    <asp:TextBox ID="txtFulName" runat="server" Width="250px" CausesValidation="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFulName"
                                                        Display="Dynamic" ErrorMessage="Full Name Required" SetFocusOnError="True" ValidationGroup="c"
                                                        ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Email:
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="250px" CausesValidation="true" MaxLength="50">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Follow the right format for Email"
                                                        ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        SetFocusOnError="True" ValidationGroup="c" ForeColor="red" BackColor="white">
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="tdRegion" align="left" style="height: 22px">*Region:
                                                    <asp:DropDownList ID="ddlRegion" runat="server" Width="256px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Center:
                                                    <asp:DropDownList ID="ddlCenter" runat="server" Width="256px" CausesValidation="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Department:
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CausesValidation="False">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDepartment"
                                                        Display="Dynamic" ErrorMessage="Deepartment Required" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">*Designation:
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="256px" CausesValidation="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDesignation"
                                                        Display="Dynamic" ErrorMessage="Designation Required" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">Marital Status:
                                                    <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="100px" CausesValidation="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="M">MARIED</asp:ListItem>
                                                        <asp:ListItem Value="S">UNMARIED</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 22px">*Gender:
                                                    <asp:DropDownList ID="ddlGender" runat="server" Width="100px" CausesValidation="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="M">Male</asp:ListItem>
                                                        <asp:ListItem Value="F">Female</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlGender"
                                                        Display="Dynamic" ErrorMessage="Gender Required" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">Date Of Birth:
                                                    <asp:TextBox ID="txtDOB" runat="server" MaxLength="30" CssClass="datepicker" CausesValidation="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">Date Of Joining:
                                                    <asp:TextBox ID="txtDOJ" runat="server" MaxLength="30" CssClass="datepicker" CausesValidation="false">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trInactive">
                                                <td align="left" style="height: 22px">*Is Resigned:
                                                    <asp:DropDownList ID="ddlInactive" runat="server" Width="100px" CausesValidation="true"
                                                        OnSelectedIndexChanged="ddlresigned_click" AutoPostBack="true">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="n">No</asp:ListItem>
                                                        <%-- n--> Employee is active --%>
                                                        <asp:ListItem Value="y">Yes</asp:ListItem>
                                                        <%-- y--> Employee has resigned --%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInactive"
                                                        Display="Dynamic" ErrorMessage=" Please select a Value " InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trResign" visible="false">
                                                <td align="left">*Date Of Resignation:
                                                    <asp:TextBox ID="txtResignDate" runat="server" MaxLength="30" CssClass="datepicker"
                                                        CausesValidation="true" ValidationGroup="c">
                                                    </asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtResignDate"
                                                        Display="Dynamic" ErrorMessage="Resignation Date Required" SetFocusOnError="True"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <%--  <tr  runat="server"  id="trRejoin" visible="false" >
                                                <td align="left">
                                                Date Of Rejoining*:
                                                <asp:TextBox ID="txtRejoin" runat="server" MaxLength="30" CssClass="datepicker" CausesValidation="true" ValidationGroup="c">
                                                </asp:TextBox>
                                    
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRejoin"
                                                Display="Dynamic" ErrorMessage="Rejoin Date Required"   SetFocusOnError="True"
                                                ValidationGroup="c" ForeColor="red" BackColor="white" ></asp:RequiredFieldValidator></td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" EnableClientScript="true"
                                                        HeaderText="Please ensure you have updated the following correctly:" runat="server"
                                                        ValidationGroup="c" ForeColor="red" BackColor="white" />
                                                </td>
                                            </tr>
                                            <tr style="height: 3px">
                                                <td style="text-align: center">
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CausesValidation="True"
                                                        CssClass="btn round_corner" ValidationGroup="c" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                        CausesValidation="False" CssClass="btn round_corner" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="divLeavebal" class="new_event">
                            <div class="panel">
                                <div class="panel_head">
                                    <p>
                                        Employee Leave(s) Balance
                                    </p>
                                    <div class="new_event">
                                        <table class="new_event" style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" style="width: 50%;">Casual Leave*:&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtcasual" runat="server" MaxLength="10" class="text-primary" CausesValidation="true"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                        ControlToValidate="txtcasual" ValidationExpression="((\d+)((\.\d{1,2})?))$" ValidationGroup="e">
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width: 50%;">Annual Leave*:
                                                </td>
                                                <td align="left" style="width: 50%; margin-left: 5px">
                                                    <asp:TextBox ID="txtannual" runat="server" MaxLength="10" class="text-primary" CausesValidation="true">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Accepts only integers."
                                                        ControlToValidate="txtannual" ValidationExpression="^[0-9]*$" ValidationGroup="e">
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">Remarks:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtRemarks" runat="server" MaxLength="30" class="text-primary" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSaveLeave" runat="server" CssClass="btn round_corner" CausesValidation="true"
                                                        OnClick="btnSave_Click" Text="Save" />
                                                    <asp:Button ID="btnCancelLeave" runat="server" CausesValidation="False" CssClass="btn round_corner"
                                                        OnClick="btnCancel_Click" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" class="panel" id="ResignedEmployees">
                            <div class="panel_head">
                                List of Inactive Employees
                            </div>
                            <div class="panel_body">
                                <asp:GridView ID="gv_ResignedEmployees" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" EmptyDataText="No Data Exist!" OnPageIndexChanging="gv_ResignedEmployees_PageIndexChanging"
                                    OnSorting="gv_ResignedEmployees_Sorting" AllowPaging="True" AllowSorting="True"
                                    PageSize="15">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="EmployeeCode" SortExpression="EmployeeCode"></asp:BoundField>
                                        <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="EmployeeCode">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="EmployeeCode">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FullName" HeaderText="Employee Name" SortExpression="EmployeeCode" />
                                        <asp:BoundField DataField="Region_Id" HeaderText="Region_Id" SortExpression="Region_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RegName" HeaderText="Region Name" SortExpression="Region_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Center_Id" HeaderText="Center_Id" SortExpression="Center_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CenName" HeaderText="Center Name" SortExpression="Center_Id">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                        <asp:BoundField DataField="DeptCode" HeaderText="DeptCode" SortExpression="DeptCode">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DesigCode" HeaderText="DesigCode" SortExpression="DesigCode">
                                            <ItemStyle CssClass="hide" />
                                            <HeaderStyle CssClass="hide" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptCode" />
                                        <asp:BoundField DataField="DesigName" HeaderText=" Designation" SortExpression="DesigCode" />
                                        <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" SortExpression="DOJ"
                                            DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="DOB" HeaderText="Date of Birth" SortExpression="DOB" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="MaritalSts" HeaderText="Marital Status" SortExpression="MaritalSts" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                        <asp:BoundField DataField="Inactive" HeaderText="Is Resigned" SortExpression="Inactive"></asp:BoundField>
                                        <asp:BoundField DataField="CasualLeave" HeaderText="Casual Leave" SortExpression="CasualLeave" />
                                        <asp:BoundField DataField="AnnulaLeave" HeaderText="Annual Leave" SortExpression="AnnulaLeave" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Update Employee Details">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnUpdateEmployeeDetails" runat="server" CommandArgument='<%# Eval("EmployeeCode") %>'
                                                    CausesValidation="False" CommandName="Edit Resigned" OnClick="btnUpdateResigned_Click"
                                                    ImageUrl="~/images/approval.png" Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="tr1" />
                                    <HeaderStyle CssClass="tableheader" />
                                    <AlternatingRowStyle CssClass="tr2" />
                                </asp:GridView>
                                <asp:Label ID="lblNote" runat="server" Text="No Data Exists." Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
