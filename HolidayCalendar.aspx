<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="HolidayCalendar.aspx.cs" Inherits="HolidayCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="css/examples.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.12/css/jquery.dataTables.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
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
    <script type="text/javascript">

        Sys.Application.add_init(function () {
            // Initialization code here, meant to run once.
            $.noConflict();
            jQuery(document).ready(document_Ready);


            function document_Ready() {

                jQuery(document).ready(function () {

                    //****************************************************************


                    try {
                        jQuery('table.datatable').DataTable({
                            destroy: true,
                            // sDom: 'T<"dataTables_wrapper"tfrlip>', // its ok


                            //                    dom: "<'row'<'col-sm-5'T><'col-sm-7'f>>R" +
                            dom: "<'row'<'col-sm-4'l><'col-sm-3'T><'col-sm-5'f>>R" +
                     "<'row'<'col-sm-12'tr>>" +
                            //                     "<'row'<'col-sm-12'l>>" +
                      "<'row'<'col-sm-12'i>><'row'<'col-sm-12'p>>",
                            "columnDefs": [

                            //    { orderable: false, targets: [4]} //disable sorting on toggle button
                                    ]

                                ,
                            tableTools:
                    { //Start of tableTools collection
                        "sSwfPath": "http://cdn.datatables.net/tabletools/2.2.4/swf/copy_csv_xls_pdf.swf",
                        "aButtons":
                         [ //start of button main/master collection



              { // ******************* Start of child collection for export button
              "sExtends": "collection",
              "sButtonText": "<span class='glyphicon glyphicon-export'></span>",
              "sToolTip": "Export Data",
              "aButtons":
                         [ //start of button export buttons collection

              // ******************* Start of copy button
                    {
                    "sExtends": "copy",
                    "sButtonText": "<span class='glyphicon glyphicon-copy'></span> Copy Contents",
                    "sToolTip": "Copy Data"
                      , "mColumns": [1, 2, 3, 4, 5, 6]
                } // ******************* end of copy button

              // ******************* Start of csv button
                  , {
                      'sExtends': 'csv',
                      'bShowAll': false // ,'sFileName': "DataInCSVFormat.csv"
                      ,
                      "sFileName": "DataInCSVFormat - *.csv",
                      "sToolTip": "Save as CSV",
                      //'sButtonText': 'Save as CSV',
                      "sButtonText": "<span class='fa fa-file-text-o'></span> Save to CSV",
                      "sNewLine": "auto"
                         , "mColumns": [1, 2, 3, 4, 5, 6]
                  }  // ******************* end of csv button

              // ******************* Start of excel button
                   , {
                       'sExtends': 'xls',
                       'bShowAll': false,
                       "sFileName": "DataInExcelFormat.xls",
                       //'sButtonText': 'Save to Excel',
                       "sButtonText": "<span class='fa fa-file-excel-o'></span> Save to Excel",
                       "sToolTip": "Save as Excel"
                        , "mColumns": [1, 2, 3, 4, 5, 6]
                   }  // ******************* End of excel button


              // ******************* Start of PDF button
                  , {
                      'sExtends': "pdf",
                      'bShowAll': false,
                      "sButtonText": "<span class='fa fa-file-pdf-o'></span> Save to PDF",
                      //'sButtonText': 'Save to PDF',
                      "sFileName": "DataInPDFFormat.pdf",
                      "sToolTip": "Save as PDF" //,"sPdfOrientation": "landscape"
                         , "mColumns": [1, 2, 3, 4, 5, 6]
                      //,"sPdfMessage": "Your custom message would go here."
                  } // *********************  End of PDF button 

                         ]// ******************* end of Export buttons collection
          }    // ******************* end of child of export buttons collection
                         ] // ******************* end of button master Collection
                    } // ******************* end of tableTools
         , "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]], "iDisplayLength": 25, 'bLengthChange': true // ,"bJQueryUI":true , fixedHeader: true 
         , "order": [[0, "asc"]], "paging": false, "ordering": true, "searching": true, "info": true, "scrollX": false, "stateSave": true
         , //--- Dynamic Language---------
                            "oLanguage": {
                                "sZeroRecords": "There are no Records that match your search critera",
                                //                    "sLengthMenu": "Display _MENU_ records per page&nbsp;&nbsp;",
                                "sInfo": "Displaying _START_ to _END_ of _TOTAL_ records",
                                "sInfoEmpty": "Showing 0 to 0 of 0 records",
                                "sInfoFiltered": "(filtered from _MAX_ total records)",
                                "sEmptyTable": 'No Rows to Display.....!',
                                "sSearch": "Search :"
                            }
                        }
                   );
                    }
                    catch (err) {
                        alert('datatable ' + err);
                    }

                    //****************************************************************



                }
   );

            } //end of documnet_ready()



            //Re-bind for callbacks
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                jQuery(document).ready(document_Ready);
                //            document_Ready();
                //            alert('call back done');
            }
);

        });
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="outer_wrap">
                <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                            Holiday Calendar</p>
                        <div class="controls round_corner">
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="dropdownlist" Width="130px"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlMonths_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Button runat="server" ID="btnShowDetail" CssClass="btn btn-success" Text="Show Detail"
                                OnClick="btnShowDetail_Click" />
                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-success" Text="Cancel"
                                Visible="false" OnClick="btnCancel_Click" />
                        </div>
                        <!--end controls-->
                        <div class="clear">
                        </div>
                        <div class="panel">
                            <div class="panel_head">
                                Holidays</div>
                            <!--end panel_head-->
                            <div class="panel_body" runat="server" id="divshow">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvLib" SkinID="GridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    HorizontalAlign="Center" OnPageIndexChanging="gvLib_PageIndexChanging" OnRowDataBound="gvLib_RowDataBound"
                                    OnRowDeleting="gvLib_RowDeleting" OnSorting="gvLib_Sorting" OnSelectedIndexChanging="gvLib_SelectedIndexChanging"
                                    AllowPaging="True" AllowSorting="True" EmptyDataText="No Data Exist!">
                                    <Columns>
                                        <asp:BoundField DataField="CalId" HeaderText="id">
                                            <ItemStyle CssClass="hide"></ItemStyle>
                                            <HeaderStyle CssClass="hide"></HeaderStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CalenderDate" SortExpression="CalenderDate" HeaderText="Date"
                                            DataFormatString="{0:dddd dd/MM/yyyy}" HtmlEncode="False">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Description">
                                        </asp:BoundField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Edit">
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    ImageUrl="~/images/edit.gif" Text="Edit" ToolTip="View / Edit" />
                                                &nbsp;
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="Remove">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                    ImageUrl="~/images/delete.gif" Text="" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="10px" />
                                            <ItemStyle Width="10px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lab_dataStatus" runat="server" Visible="False" Text="No Data Exists."></asp:Label>
                            </div>
                            <div class="panel_body" runat="server" id="divshowdetail">
                                <!--Paste Grid code here-->
                                <asp:GridView ID="gvdetail" SkinID="GridView" runat="server" AutoGenerateColumns="False"
                                    CssClass="datatable table table-hover" OnPreRender="gvdetail_PreRender" BorderStyle="None"
                                    HeaderStyle-BackColor="#c6c6c6">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CalenderDate" SortExpression="CalenderDate" HeaderText="Date">
                                            <ItemStyle Width="140px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RegionName" SortExpression="RegionName" HeaderText="Region Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="center_id" SortExpression="center_id" HeaderText="Center Id">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Center_Name" SortExpression="Center_Name" HeaderText="Center Name">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Description" SortExpression="Description" HeaderText="Reason">
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <!--end panel_body-->
                            <div class="panel_footer">
                                <!--end panel_footer-->
                            </div>
                        </div>
                        <div class="panel">
                            <div class="panel-body">
                                <div class="form-horizontal">
                                    <div class="new_event" runat="server" id="divAdd">
                                        <p>
                                            <asp:Label runat="server" Text="Apply to Centers" ID="lblApplytoCenter" Visible="false"></asp:Label>
                                            <asp:CheckBox runat="server" ID="cbApplyCenter" Visible="false" OnCheckedChanged="loadCenters"
                                                AutoPostBack="true" />
                                        </p>
                                        <p>
                                            Description:</p>
                                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="250" CssClass="textbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDescription"
                                            ErrorMessage="*Description Required" ValidationGroup="A" ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                        <p>
                                            Holiday Date:</p>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                                            Text="Save" ValidationGroup="A" />
                                    </div>
                                    <div class="row">
                                        <asp:GridView ID="gvCenter" runat="server" Width="100%" AutoGenerateColumns="False"
                                            HorizontalAlign="Center" SkinID="GridView" BackColor="white" CssClass="table table-hover"
                                            OnRowCommand="gvCenters_RowCommand" HeaderStyle-BackColor="#c6c6c6">
                                            <Columns>
                                                <asp:BoundField DataField="Center_ID" HeaderText="Center Id">
                                                    <ItemStyle CssClass="hide"></ItemStyle>
                                                    <HeaderStyle CssClass="hide"></HeaderStyle>
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemStyle Width="20px" />
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Center_Name" HeaderText="Center Name"></asp:BoundField>
                                                <asp:TemplateField ShowHeader="False" HeaderText="Check">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="toggleCheck">Select</asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="cbAllow" CssClass="checkbox" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end inner wrap-->
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
