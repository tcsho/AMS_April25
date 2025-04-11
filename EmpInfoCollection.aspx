<%@ Page Title="" Language="C#"   AutoEventWireup="true" CodeFile="EmpInfoCollection.aspx.cs" Inherits="EmpInfoCollection" %>

 <html>
 <head>
 
<link href="css/examples.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="css/style_new.css" type="text/css" />
<link rel="Stylesheet" href="css/jquery-ui.css" type="text/css" />
<link rel="stylesheet" href="http://code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />

    <script src="Scripts/jquery.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>

 <script type="text/javascript">

     // JavaScript Document
     $(document).ready(function (e) {

         $('.acordian_header').click(function () {
             $('.acordian_body').slideToggle("slow", function () {
                 /*$('.acordian_header p').toggleClass('plus_icon');*/
                 $('.acordian_header').find('.plus_icon').toggleClass('minus_icon');
             });
         });


      

         $("nav li").has("ul").prepend('<span class="plus_icon">Plus</span>').css("background-color", "#888").attr("title", "Click To Expand");

         /*$("nav li").css({"list-style-position":"inside","list-style-image":"url(images/icons/list_item.png)"});
         $("nav li").has("ul").css({"list-style-image":"url(images/icons/list_icon_down.png)","Font-weight":"bold"}).attr("title","Click To Expand");*/

         $("nav li").css("cursor", "pointer");


         $('nav li').click(function () {
             $(this).find('.plus_icon').toggleClass('minus_icon');
             $(this).find('ul').slideToggle(800);
         });


         /*********Navigation***********/

 


         $('.close').click(function () {
             $('.mesg').slideUp("slow");
             $('.error_mesg').slideUp("slow");
         });
         





     });

</script>
 </head>
 <body> <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="3600" runat="server">
                <Scripts>
        </Scripts>
            </asp:ScriptManager>
    
 <div class="">  

 	<div class="header fullrow">
        	<div class="inner_wrap">
                <div class="logo">
            		<img src="images/mini_logo.png" alt="Logo" />
                </div>
                <h1>Attendance Management System</h1>
                
                
                <div style="float:right;margin-right:5px;margin-top:25px;">
                    <asp:Button ID="btnLogOut" CssClass="round_corner btn" runat="server" OnClick="btnLogOut_Click" Text="LogOut" CausesValidation="false" />
                </div>
                <div class="acordian">
                    <div class="acordian_header">
                        <p>User Profile</p>
                        <p class="plus_icon">Plus</p>
                    </div><!--end acordian_header-->
                    <div class="acordian_body">

                        <p>Employee # : <asp:Label ID="lblEmp" runat="server" Text="" ></asp:Label></p>
<p>Name : <asp:Label ID="lblName" runat="server" Text="" ></asp:Label></p>
<p>Region : <asp:Label ID="lblRegion" runat="server" Text="" ></asp:Label></p>
<p>Center : <asp:Label ID="lblCenter" runat="server" Text="" ></asp:Label></p>

                        <table border="0" cellpadding="0" cellspacing="0" align="center">
                           
                        </table>
                        
                         
                        
                         
                    </div><!--end acordian_body-->
                </div><!--end acordian-->
                
            </div><!--end inner_wrap-->
        </div><!--end header-->
             </div>  
                <div class="outer_wrap">
               <div class="fullrow">
                    <div class="inner_wrap">
                        <p class="page_heading">
                           Employee Information Collection</p>
                            <asp:Panel ID="pan_New" runat="server" class="panel new_event_wraper " Style="display: inline;">
                          
                            
                            <div class="new_event">
                                 <p>
                                    Official Email :
                                </p>
                                <asp:TextBox ID="txtEmail" runat="server" ViewStateMode="Enabled" placeholder="FirstName.LastName@csn.edu.pk" ></asp:TextBox>
                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
              runat="server" ErrorMessage="Please Enter Valid Official Email Id e.g. example@csn.edu.pk"
                  ValidationGroup="c" ControlToValidate="txtEmail"
                  CssClass="requiredFieldValidateStyle"
                  ForeColor="Red"
                  ValidationExpression="\w+([-+.']\w+)*@csn.edu.pk">
                  </asp:RegularExpressionValidator>--%>

                   <p>
                                    Cell # :
                                </p>
                                <asp:TextBox ID="txtCell" runat="server" placeholder="03211234567" MaxLength="11" ></asp:TextBox>
                               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"   
ControlToValidate="TextBox3" ErrorMessage="Not a Valid Phone#."   
ValidationExpression="^(\(?\s*\d{3}\s*[\)\-\.]?\s*)?[2-9]\d{2}\s*[\-\.]\s*\d{4}$"></asp:RegularExpressionValidator>  
--%>
                                  
                                   <p>
                                    Landline # With Area Code :
                                </p>
                                <asp:TextBox ID="txtLandline" runat="server" placeholder="04231234567" MaxLength="11" ></asp:TextBox>
                                  

                                  <p>
                                    Office Ext # :</p>
                                    <asp:TextBox ID="txtExt" runat="server"  MaxLength="5" placeholder="123"  ></asp:TextBox>
                               
                             <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="button"
                                    ValidationGroup="c" Text="Submit"></asp:Button>
                                 </div>
                         
                        </asp:Panel>
  </div>
                             </div>
                    <!--end inner wrap-->
                    </div>
              </form>  
 </body></html>
         
          