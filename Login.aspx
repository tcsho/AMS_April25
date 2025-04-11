<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance Management System</title>
    <link rel="stylesheet" type="text/css" href="css/login.css" />



    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-impromptu/6.2.3/jquery-impromptu.css" />

    <script src="Scripts/jquery.js" type="text/javascript"></script>

    <script src="Scripts/jquery-impromptu.2.7.min.js" type="text/javascript"></script>


    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1">
</head>
<body>
    <div class="container-fluid" style="margin-top: 10%">
        <div class="row">
            <div class="col-xs-6 col-sm-3 col-md-2 col-lg-3 col-xl-4">
            </div>

            <div class="col-xs-10 col-sm-6 col-md-8 col-lg-6  col-xl-5">

                <form id="form1" runat="server">
                    <div class=" wraper">
                        <div class="body_sec">
                            <div class=" login_form_container ">
                                <div class="logo">
                                    <img src="images/citylogo_widouttext.png" />
                                </div>
                                <div class="Header_login_form">
                                    <p><span class="heading">AMS</span> Attendance Management System</p>
                                </div>
                                <div class="body_login_form">
                                    <h1>Sign in</h1>
                                    <asp:TextBox ID="text_login" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="text_password" runat="server" TextMode="password">Password</asp:TextBox>

                                    <br />
                                    <asp:Label ID="lab_status" runat="server" ForeColor="White" Font-Size="Large" Font-Names="tahoma"></asp:Label>

                                    <asp:Button ID="btnlogin" runat="server" Text="Log In" CssClass="btn btn-danger"
                                        OnClick="btnlogin_Click1" />


                                </div>
                            </div>
                            <!--end login_form_container-->
                        </div>
                        <!--end body_sec-->
                    </div>
                    <!--end content_wrap-->


                </form>

            </div>

        </div>
    </div>

</body>
</html>
