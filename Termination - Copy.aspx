<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Termination.aspx.cs" Inherits="Termination" %>

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
            color: #ffff00;
            margin-bottom: 5px;
            margin-left: 2em;
        }

        .style-changes td{
                vertical-align: top;
            }
            .style-changes textarea{
                resize: vertical;
            }
    </style>
    <script type="text/javascript">
        //function BindEvents() {
        //$(document).ready(function () {

        $(document).ready(document_Ready);

        function document_Ready() {
            $('.show_hide_btn').click(function () {
                $('.approved').slideToggle()
            });

            /*Date Picker*/
            $(function () {
                //$(".datepicker").datepicker();
                $('.datepicker').datepicker({
                    dateFormat: "dd/mm/yy"
                    //onSelect: function () {
                    //    $(this).val(); //this will return the date ex: 29/07/2019

                    //}
                });
                $("#anim").change(function () {
                    $(".datepicker").datepicker("option", "showAnim", $(this).val());
                });

                
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
                <%--<div class="fullrow">
                    <div class="inner_wrap">
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlEmp" runat="server" CssClass="dropdownlist" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <!--end controls-->
                        <div class="clear">
                        </div>
                    </div>
                    <!--end inner wrap-->
                </div>--%>
                <!--end fullrow-->
                <div class="body_content fullrow">
                    <div class="inner_wrap">
                        <asp:UpdatePanel ID="upLateArrivals" runat="server">
                            <%--Late Arrivals records--%>
                            <ContentTemplate>


                                <div class="panel">
                                    <%-- ****************************** drop down section  *****************************--%>
                                    <div class="fullrow">
                                        <div style="width: 40%; color: white; text-align:center; font-size:25px;">Termination Form</div>
                                <br />
                                        <div class="inner_wrap" style="margin-left: 40px">
                                            <div class="controls round_corner">
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
                                    <asp:Panel ID="employeeDetailsDiv" runat="server">
                                        <div style="margin: 0 auto;">
                                            
                                            <%-- <div style="color: white; font-size: large; font-family: cursive;">Total leaves for year 2023 : 45</div>--%>
                                            <table class="style-changes" style="width: 700px;" cellspacing="0" cellpadding="0" align="center">
                                                <tr id="Tr1" runat="server">
                                                    <td style="width: 5%">&nbsp;
                                                    </td>
                                                    <td>
                                                        <table style="width: 100%; margin-left: 5px" cellspacing="0" cellpadding="0">
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; color: rgba(255,255,0,1);">Employee name:
                                                                </td>
                                                                <td align="left" style="color: white">
                                                                    <asp:Label ID="employeeNameText" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; color: rgba(255,255,0,1);">Designation:
                                                                </td>
                                                                <td align="left" style="color: white">
                                                                    <asp:Label ID="designationText" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; color: rgba(255,255,0,1);">Department:
                                                                </td>
                                                                <td align="left" style="color: white">
                                                                    <asp:Label ID="departmentText" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; color: rgba(255,255,0,1);">Region:
                                                                </td>
                                                                <td align="left" style="color: white">
                                                                    <asp:Label ID="regionText" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; color: rgba(255,255,0,1);">Center:
                                                                </td>
                                                                <td align="left" style="color: white">
                                                                    <asp:Label ID="centerText" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                    <asp:Label ID="employeeEmail" Visible="false" runat="server" MaxLength="100" CssClass="textbox"
                                                                        ReadOnly="True"></asp:Label>
                                                                </td>

                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="2"></td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </asp:Panel>

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
                                                            <td style="width: 40%; color: rgba(255,255,0,1);">Termination date*:
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="submissionDate" runat="server" Width="140px" CssClass="datepicker"
                                                                    AutoPostBack="True" OnTextChanged="resignationSubmDate_TextChanged"  DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>&nbsp;
                                                        
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="submissionDate"
                                                            Display="Dynamic" ErrorMessage="*Select Resignation Submission Date" InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 3px">
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 40%; color: rgba(255,255,0,1);">Last working date*:
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="lastDayDate" runat="server" Width="140px" CssClass="datepicker" AutoPostBack="True"
                                                                    OnTextChanged="lasDayDate_TextChanged" DataFormatString="{0:dd/MM/yyyy}"></asp:TextBox>&nbsp; 
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lastDayDate"
                                                            Display="Dynamic" ErrorMessage="*Select To Date " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 3px">
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 40%; color: rgba(255,255,0,1);">Notice period:
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="noticeDays" runat="server" Width="50px" MaxLength="100" CssClass="textbox"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 3px">
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr style="height: 3px">
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 40%; color: rgba(255,255,0,1);">Reason of termination*:
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlreason" runat="server" Width="142px" Visible="true"
                                                                    AutoPostBack="True" CssClass="dropdownlist" OnSelectedIndexChanged="ddlreason_SelectedIndexChanged">
                                                                    <asp:ListItem Enabled="true" Text="Select Reason" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="Employee transfer" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Gross misconduct" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Reinstatment" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Resigned" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="Terminated" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="Terminated on performance" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="Contract ended" Value="7"></asp:ListItem>
                                                                    <%--<asp:ListItem Text="Deceased" Value="8"></asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlreason"
                                                                    Display="Dynamic" ErrorMessage="*Select Termination Reason " InitialValue="0" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 3px">
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 40%; color: rgba(255,255,0,1);" valign="top">Comments by head:
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="hodComments" runat="server" Width="95%" Height="50px" MaxLength="500"
                                                                    Rows="15" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="hodComments"
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
                                                        CssClass="button" OnClick="btnSubmitTermination_Click" OnClientClick="return ConfirmAttachment()" />
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
                </div>
                <!--end body_content-->
            </div>
            <!--end outer_wrap-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
