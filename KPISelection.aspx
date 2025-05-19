<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KPISelection.aspx.cs" Inherits="KPISelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <%--<script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>--%>

    <style type="text/css">
        .new_event {
            float: right;
            width: 70%;
            padding: 1% 5%;
        }
        .btn-primary {
    min-width: 180px;
    padding: 10px 15px;
    font-weight: bold;
    border-radius: 6px;
}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="fullrow">
                <div class="inner_wrap">
                    <%--<h1 class="page_heading">Change Password</h1>--%>

                    <div class="panel">
                        <div class="panel_head">
                            Please select the action you want to perform 
                        </div>
                        <div class="panel_head">
     <div style="display: flex; justify-content: center; align-items: center; gap: 20px;">
     <asp:Button ID="btnMngKPI" runat="server" Visible="false"
         CssClass="btn btn-primary" Text="Manage KPI Templates" OnClick="btnMngKPI_Click" />
     <asp:Button ID="btnAsgnKPI" runat="server" Visible="false"
         CssClass="btn btn-primary" Text="Assign KPI Templates" OnClick="btnAsgnKPI_Click" />
     <asp:Button ID="btnUpdKPI" runat="server" Visible="false" 
         CssClass="btn btn-primary" Text="Update Employee KPIs" OnClick="btnUpdKPI_Click" />
 </div>
</div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

