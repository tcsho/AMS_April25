<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LmsAppMenu.aspx.cs" Inherits="LmsAppMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



        <div class="outer_wrap">
            
            <div class="fullrow">
            
                <div class="inner_wrap">
                
                    <p class="page_heading">Manage Application Menues</p>
                    

                    <div class="controls">
                    
                        <asp:LinkButton ID="but_new" OnClick="but_new_Click1" runat="server" CausesValidation="False" CssClass="btn">Add New Menu</asp:LinkButton>

                    </div>



                    <asp:Panel ID="pan_New" runat="server"  class="panel new_event_wraper">

                            <div class="new_event">

                                

                                
                                                <asp:Label ID="error" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label></td>
                                            
                                                <p>Menu Name*:</p>
                                           
                                                <asp:TextBox ID="txt_CName" runat="server" CssClass="textbox" MaxLength="50" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_CName" ErrorMessage="Menu Name is Required Field." ForeColor="DarkRed"></asp:RequiredFieldValidator>
                                                   
                                                        
                                                <p>Parent Menu*:</p>

                                                        <asp:DropDownList ID="ddlParentMenu" runat="server" >
                                                        </asp:DropDownList>
                                                        

                                            
                                                <p>Page *:</p>

                                                        <asp:DropDownList ID="ddlPage" runat="server" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="s"
                                                            Display="Dynamic" ErrorMessage="Page is required" ControlToValidate="ddlPage"
                                                            InitialValue="0"></asp:RequiredFieldValidator>

                                            
                                                <asp:Button ID="but_save" OnClick="but_save_Click" runat="server"
                                                    Text="Save"></asp:Button>

                                                <asp:Button ID="but_cancel" OnClick="but_cancel_Click" runat="server" 
                                                    CausesValidation="False" Text="Cancel"></asp:Button>




                            </div>

                    </asp:Panel>





                    <div class="panel" >
                        <div class="panel_head">
                            Existing Menues
                        </div>

                        <div class="panel_body" style="max-height:100%; overflow:auto;">


                        <asp:GridView ID="dv_country" runat="server" Width="100%" AutoGenerateColumns="False" 
                                HorizontalAlign="Center" OnPageIndexChanging="dv_country_PageIndexChanging" OnRowDeleting="dv_country_RowDeleting"
                                OnSorting="dv_country_Sorting" OnRowDataBound="dv_country_RowDataBound" OnSelectedIndexChanging="dv_country_SelectedIndexChanging"
                                AllowPaging="True" AllowSorting="True" PageSize="50">
                                <Columns>                                
                                
                                    <asp:BoundField DataField="Menu_Id" HeaderText="id">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PageId" HeaderText="Pid">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="PrntMenu_Id" HeaderText="Pmid">
                                        <ItemStyle CssClass="hide" />
                                        <HeaderStyle CssClass="hide" />
                                    </asp:BoundField>
                                    
                                    
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MenuText" HeaderText="MenuName" SortExpression="MenuText" />
                                    <asp:BoundField DataField="ParentMenuText" HeaderText="Parent Menu" SortExpression="ParentMenuText" />
                                    
                                    <asp:BoundField DataField="PageTitle" HeaderText="Page Title" SortExpression="PageTitle" />
                                    <asp:TemplateField ShowHeader="False" HeaderText="Edit/Delete">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="Update" />
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                ImageUrl="~/images/edit.gif" Text="Edit" />
                                            &nbsp;
                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                ImageUrl="~/images/delete.gif" Text="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     
                                    
                                </Columns>
                                <RowStyle CssClass="tr1" />
                                <HeaderStyle CssClass="tableheader" />
                                <AlternatingRowStyle CssClass="tr2" />
                                <SelectedRowStyle CssClass="tr_select" />
                            </asp:GridView>
                            <asp:Label ID="lab_dataStatus" runat="server" Text="No Data Exists." Visible="False"></asp:Label>






                        </div>

                        <div class="panel_footer">
                        </div>
                    </div>

                </div>
            
            </div>

        </div>



            
            <cc1:ModalPopupExtender ID="MPopEx" runat="server" TargetControlID="hiddenForPopUp"
                Enabled="false">
            </cc1:ModalPopupExtender>
            <asp:Button Style="display: none" ID="hiddenForPopUp" runat="server"></asp:Button>
            <asp:Panel ID="msgBox" runat="server" Visible="False" Width="400">
                <table cellspacing="0" cellpadding="0" width="400" border="0">
                    <tbody>
                        <tr>
                            <td style="background-repeat: no-repeat; height: 25px" valign="middle" background="../images/popup_top.png">
                                <asp:Panel Style="cursor: move" ID="msgDrag" runat="server" Width="100%" Height="25px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="height: 25px">
                                            </td>
                                            <td style="height: 25px;" align="right">
                                                <asp:Image Style="cursor: pointer;" ID="msgCross" runat="server" ImageUrl="~/images/btncross.jpg" />
                                            </td>
                                            <td style="height: 25px; width: 2px;">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="background-repeat: repeat-y" background="../images/popup_center.jpg">
                                <table style="width: 100%" cellspacing="0" cellpadding="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 1px; height: 14px">
                                            </td>
                                            <td style="width: 10px; height: 14px">
                                            </td>
                                            <td style="width: 5px; height: 14px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px">
                                            </td>
                                            <td>
                                                <asp:Label ID="msgNote" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 1px; height: 17px">
                                            </td>
                                            <td style="width: 10px; height: 17px">
                                            </td>
                                            <td style="width: 5px; height: 17px">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-repeat: repeat-y" align="right" background="../images/popup_center.jpg">
                                <asp:Button ID="msgOK" runat="server" Width="75px" Text="OK"></asp:Button>
                                <asp:Button ID="msgNo" runat="server" Width="75px" Text="No" Visible="False"></asp:Button>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img height="4" src="../images/popup_bot.png" width="400" /></td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

