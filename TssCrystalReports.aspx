<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TssCrystalReports.aspx.cs" Inherits="TssCrystalReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<%--    <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
    <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance Management System</title>
    <link rel="stylesheet" href="style.css" type="text/css" />
    <link rel="shortcut icon" href="../favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="css/style.css" type="text/css" media="screen" />
    <style type="text/css">
			span.reference{
				position:fixed;
				left:10px;
				bottom:10px;
				font-size:12px;
			}
			span.reference a{
				color:#aaa;
				text-transform:uppercase;
				text-decoration:none;
				text-shadow:1px 1px 1px #000;
				margin-right:3px;
			}
			span.reference a:hover{
				color:#ddd;
			}
			ul.sdt_menu{
				margin-top:0px;

			}
		</style>

    <script language="javascript" type="text/javascript">

        var isClose = 0;
        var needToConfirm = true;

        window.onload = function () {
            isClose = 0;
        }
        window.onbeforeunload = function () {
            __doPostBack('rptviewer', 'Hello Post Back');
        }

        function OnTab() {
            isClose = 1;
            __doPostBack('rptviewer', 'Hello Post Back');
        }
    </script>

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%; height: 768px; background-image: url('Images/bggrey.png')"
            cellpadding="0" cellspacing="0" width="100%">
            <tr style="width: 100%; height: 8px; background-color: #8d8b8b;">
                <td style="width: 10%; height: 6px;">
                </td>
                <td style="width: 80%; height: 6px;">
                </td>
                <td style="width: 10%; height: 6px;">
                </td>
            </tr>
            <!-- Header-->
            <tr style="width: 100%; height: 8px; background-color: #8d8b8b;">
                <td style="width: 10%; height: 6px;">
                </td>
                <td style="width: 80%; height: 6px;">
                </td>
                <td style="width: 10%; height: 6px;">
                </td>
            </tr>
            <!-- Menu -->
            <!--Body-->
            <tr style="width: 100%; height: 720px; padding-top: 10px; margin-top: 10px">
                <td style="width: 10%;" align="center" valign="top">
                    <table style="height: 100%" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 2%; background-color: #8d8b8b;">
                            </td>
                            <td style="width: 96%;" align="center" valign="top">
                                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" /></td>
                            <td style="width: 2%;">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 80%;">
                    <table cellpadding="0" cellspacing="0" style="margin-left: .5%; margin-top: 1%; margin-bottom: 1%;
                        background-image: url('Images/innercontainerbg.jpg'); border-style: solid; border-color: Silver;
                        border-width: 3px" width="99%">
                        <tr align="center">
                            <td>
                                <CR:CrystalReportViewer ID="rptviewer" runat="server" AutoDataBind="true" OnNavigate="rptviewer_Navigate"
                                    EnableDrillDown="False" />
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%;">
                    <table style="height: 100%" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 2%;">
                            </td>
                            <td style="width: 96%;">
                            </td>
                            <td style="width: 2%; background-color: #8d8b8b;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!--Footer-->
            <tr style="width: 100%; height: auto">
                <td style="width: 10%; background-color: #8d8b8b;">
                </td>
                <td style="width: 80%">
                    <table style="width: 100%; background-color: #8d8b8b;">
                        <tr>
                            <td>
                                &nbsp;<span style="color: #ffffff"><span style="font-family: Arenski">The City School</span>
                                    Attendance Management</span></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%; background-color: #8d8b8b;">
                </td>
            </tr>
            <tr style="width: 100%; height: 8px; background-color: #8d8b8b;">
                <td style="width: 10%; height: 6px;">
                </td>
                <td style="width: 80%; height: 6px;">
                </td>
                <td style="width: 10%; height: 6px;">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
