<%@ Page Title="" Language="C#"
    MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true"
    CodeFile="ChPass.aspx.cs" Inherits="ChPass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <%--<script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>--%>

    <style type="text/css">
        .new_event {
            float: right;
            width: 70%;
            padding: 1% 5%;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="fullrow">
                <div class="inner_wrap">
                    <%--<h1 class="page_heading">Change Password</h1>--%>

                    <div class="panel">
                        <div class="panel_head">
                            Change Password
                        </div>
                        <div class="form-horizontal">
                            <div class="new_event">

                                <div class="row">
                                    <div class="form-group col-xl-12">
                                        <p class="LabelWhite">
                                            User*: 
                                        </p>

                                        <asp:TextBox ID="txtUser" runat="server" MaxLength="50" Enabled="False" CssClass="form-control"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-xl-12">
                                        <p class="LabelWhite">New Password:</p>

                                        <asp:TextBox ID="txtNPass" runat="server" MaxLength="50" ValidationGroup="S" placeholder="type password"
                                            TextMode="Password" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNPass"
                                            ErrorMessage="*New Password Required" ValidationGroup="A" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-xl-12">
                                        <p class="LabelWhite">
                                            Confirm New Pasword:
                                        </p>
                                        <asp:TextBox ID="txtNPassC" runat="server" MaxLength="50" TextMode="Password" placeholder="confirm password" CssClass="form-control"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNPassC"
                                            ControlToValidate="txtNPass" ErrorMessage="CompareValidator" ValidationGroup="A">New Passwords does not matched!</asp:CompareValidator>
                                    </div>
                                    <div class="form-group col-xl-12">
                                        <div class="row">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                                                    CssClass="btn btn-primary" Text="Save" ValidationGroup="A" Width="20%" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>