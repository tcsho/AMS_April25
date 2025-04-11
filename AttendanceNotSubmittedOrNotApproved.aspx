<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AttendanceNotSubmittedOrNotApproved.aspx.cs" Inherits="AttendanceNotSubmittedOrNotApproved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>

<div class="inner_wrap">
        <div class="Reoprts_list_body">
          <asp:Button ID="btnViewReport" class="button" runat="server" Text="View" CssClass="show_report_btn btn round_corner"
                                                    Width="58px" OnClick="btnViewReport_Click" />
        <table cellpadding="0" cellspacing="0" width="100%">
                        
                        
                        <tr class="tr2">
                        <td style="width:30%;color:#FFF;" align="left" valign="top" >
                            <asp:RadioButtonList BorderWidth="2px" BorderColor="silver" Width="100%" ID="rbLstRpt" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbLstRpt_SelectedIndexChanged" >
                                <asp:ListItem Selected="True" Value="0">Atendance Not Submitted Or Not Approved</asp:ListItem>
                            </asp:RadioButtonList></td>
                        
                        <td valign="top" style="">
                            <table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                <tr id="1">
                                    <td style="width: 495px">
                                        
                                                <%--<table style="width: 100%;" cellspacing="0" cellpadding="0" border="0">
                                                    <tr id="trMonth" runat="server" >
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            </td>
                                                        <td colspan="4" align="left">
                                                            </td>
                                                    </tr>
                                                    <tr style="height: 3px">
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr id="trFrmDate" runat="server" visible = "false">
                                                        <td align="right" style="width: 20%; text-align: left;">
                                                            Attendance Date:</td>
                                                        <td colspan="4" align="left">
                                                            <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="30" Width="125px" CssClass="datepicker"></asp:TextBox>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr style="height: 3px">
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                </table>--%>
                                          
                                    </td>
                                </tr>
                                <tr style="height: 5px">
                                    <td colspan="5" style="height: 5px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        
                        </tr>
                        
                        
                        </table>



        </div>

            <div class="Reoprts_list_footer">
            	<p class="Show_reoprt_icon">click to Down</p>
            </div>
            

</div>

       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


