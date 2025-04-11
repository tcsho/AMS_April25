<%@ Page Title="" Language="C#" MasterPageFile="~/AMS.master" AutoEventWireup="true"
    CodeFile="ProcessAttendance.aspx.cs" Inherits="ProcessAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="fullrow">
                <div class="inner_wrap">
                    <h1 class="page_heading">Employee Daily Attendance Process</h1>
                    <div class="panel">
                        <div class="panel_head">
                            Employee Daily Attendance Process
                        </div>
                        <div class="form-horizontal">
                            <div class="new_event">

                                <div class="row">

                                    <div class="form-group col-xl-12">
                                        <p>
                                            Month:
                                        </p>
                                        <asp:DropDownList ID="ddlMonths" runat="server" Visible="true" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                    <div id="div_region" class="form-group col-xl-12" runat="server">

                                        <p>
                                            Region:
                                        </p>
                                        <asp:DropDownList ID="ddl_region" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_region_SelectedIndexChanged">
                                        </asp:DropDownList>

                                    </div>


                                    <div id="div_center" runat="server" class="form-group col-xl-12">

                                        <p>
                                            Center:
                                        </p>
                                        <asp:DropDownList ID="ddl_center" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>

                                    </div>


                                    <br />

                                    <br />
                                    <div class="form-group col-xl-12">

                                        <p>
                                            Process Attendance:
                                        </p>
                                        <asp:Button ID="btnProcess" runat="server" CssClass="btn form-control" OnClick="btnSave_Click"
                                            Text="Process Attendance" ValidationGroup="A" Width="100%" />
                                       
                                        <p>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </p>

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
