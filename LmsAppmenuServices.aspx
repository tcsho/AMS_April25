<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LmsAppmenuServices.aspx.cs" Inherits="LmsAppmenuServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


        <div class="outer_wrap">
            
            <div class="fullrow">
            
                <div class="inner_wrap">
                
                    <p class="page_heading">Manage Menu Permissions</p>
                    

                    <div class="controls">
                        <p>Select User Role:*</p>
                        <asp:DropDownList ID="list_groupName" runat="server" 
                            OnSelectedIndexChanged="list_groupName_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </div>



                    <div class="panel">
                        <div class="panel_head">
                            Menues List
                        </div>

                        <div runat="server" id="treeSection">
                            <asp:TreeView ID="MenuTreeView" runat="server" ShowCheckBoxes="All" >
                            </asp:TreeView>
                        </div>
                    

                     <asp:Button ID="btnSave" runat="server" CssClass="button" Width="58px" ValidationGroup="s"
                                        Text="Save" OnClick="btnSave_Click"></asp:Button>


                    </div>





                </div>
            </div>
        </div>

        



            <table align="center" border="0" cellpadding="0" cellspacing="0" class="main_table"
                width="100%">
                <tr style="height: 10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="titlesection" colspan="5">
                        Menues List</td>
                </tr>
                <tr style="height: 5px">
                    <td colspan="5">
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        
                    </td>
                </tr>
                <tr style="height: 5px">
                    <td colspan="5">
                    </td>
                </tr>
                <tr id="treeSection1" runat="server" style="display: none">
                    <td colspan="5" style="width: 100%">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    
                                    
                                    
                                </td>
                            </tr>
                            <tr style="height: 5px">
                                <td colspan="5">
                                </td>
                            </tr>
                            <tr style="width:100%">
                                <td align="center" style="width: 100%" colspan="5">
                                    <%--<asp:Button ID="btnSave" runat="server" CssClass="button" Width="58px" ValidationGroup="s"
                                        Text="Save" OnClick="btnSave_Click"></asp:Button>--%>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                       
                    </td>
                </tr>
            </table>
            <br />
            <%--Popupfor  Message Box --%>
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
                                <asp:Button ID="msgNo" runat="server" Width="75px" Visible="False" Text="No"></asp:Button>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

