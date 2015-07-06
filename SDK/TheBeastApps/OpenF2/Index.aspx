<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="OpenF2.Index" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>The Beast Apps</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/FinalStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="css/bootstrap/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">

        function validateEmail() {
            if (validateMailAddress(document.getElementById('<%= txtForgetEmail.ClientID %>'))) {
                return true;
            }
            alert("invalid email id");
            return false;
        }

        function validateMailAddress(objectId) {
            var obj;
            if (typeof objectId == 'object') {
                obj = document.getElementById(objectId.id);
                if (obj.value != "") {
                    var strmail = obj.value;
                    var pattern = "^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";
                    if (!strmail.match(pattern)) {
                        return false;
                    }
                    return true;
                }
                return false;
            }
        }
    </script>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <!--[if lt IE 7]>
            <p class="chromeframe">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> or <a href="http://www.google.com/chromeframe/?redirect=true">activate Google Chrome Frame</a> to improve your experience.</p>
        <![endif]-->
        <div class="row-fluid">
            <div class="span12">
                <!--Header-->
                <div class="row-fluid" style="background: url(images/body_bg.jpg) repeat-x top left;">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span4">
                                <img src="images/beastlogo-1.png" class="pull-left" style="padding: 10px 0 3px 1px;"
                                    alt="" />
                            </div>
                            <div class="span8">
                                <div class="navbar navbar-inverse pull-right">
                                    <div class="container">
                                        <!-- .btn-navbar is used as the toggle for collapsed navbar content -->
                                        <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                                            class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                                        </a>
                                        <!-- Everything you want hidden at 940px or less, place within here -->
                                        <!--<div class="nav-collapse collapse" style="padding-left: 25px; float: left;">-->
                                        <div class="nav-collapse collapse">
                                            <ul class="nav menuborde">
                                                <div class="row-fluid visible-desktop">
                                                    <div class="span12">
                                                    </div>
                                                </div>
                                                <li><a href="http://www.thebeastapps.com/">Home</a></li>
                                                <li class="active"><a href="Index.aspx">The App Store</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/DevZone">Dev Zone</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/ProductsServices">Products &amp; Services</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/ManagementTeam">Management Team</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/Contact">Contact Us</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
                <div class="row-fluid" style="background-color: #353535;">
                    <div class="span8 offset2">
                        <h2 style="color: #FFFFFF;">THE BEAST APP STORE</h2>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
                <!--Body-->
                <div class="row-fluid">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span3" style="padding: 10px 0; line-height: 20px;">
                                <div class="row-fluid">
                                    <div class="span12" id="dvUserLogin">
                                        <asp:UpdatePanel ID="upnlUserLogin" runat="server">
                                            <ContentTemplate>
                                                <asp:MultiView ID="mvUserLogin" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="v_UserLogin" runat="server" OnActivate="v_UserLogin_Activate">
                                                        <div class="control-group">
                                                            <label class="control-label" for="txtSignInUserID">
                                                                Email</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtSignInUserID" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                                    ToolTip="User ID (e.g. userid@thebeastapps.com)" placeholder="Email" CssClass="span12" />
                                                                <asp:RequiredFieldValidator ID="rfielduid" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                                    Display="None" ControlToValidate="txtSignInUserID" ErrorMessage="Please enter email id"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="valgroupNewsLetterSignin"
                                                                    runat="server" Display="None" ControlToValidate="txtSignInUserID" ToolTip="Enter Valid Email Id"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Please enter valid email id"></asp:RegularExpressionValidator>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label" for="txtSignInUserPass">
                                                                Password</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtSignInUserPass" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                                    TextMode="Password" ToolTip="Password" placeholder="Password" CssClass="span12" />
                                                                <asp:RequiredFieldValidator ID="rfieldpwd" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                                    Display="None" ControlToValidate="txtSignInUserPass" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:Button ID="btnSignIn" runat="server" Text="Sign in" CssClass="btn btn-inverse"
                                                                    OnClick="btnSignIn_Click" ValidationGroup="valgroupNewsLetterSignin" />
                                                                <asp:ValidationSummary ID="vsSign" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                                    ShowMessageBox="true" ShowSummary="false" />
                                                                &nbsp;&nbsp;<%-- <a href="#" onclick="forgotPwd_Click();" style="font-size: 85%;">Forgot
                                                                Password?</a>--%><asp:LinkButton ID="lbtnForgotPwd" runat="server" Text="Forgot Password ?"
                                                                    Style="font-size: 85%;" OnClick="lbtnForgotPwd_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls" style="line-height: 15px;">
                                                                <asp:Label ID="lblSigninMsg" ForeColor="Red" runat="server" Font-Names="verdana"
                                                                    Font-Size="Small"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="v_UserForgotPwd_EmailInput" runat="server" OnActivate="v_UserForgotPwd_EmailInput_Activate">
                                                        <%--Enter Your Registered Email Address to Receive Your Password--%>
                                                        <div class="control-group">
                                                            <div class="controls" style="line-height: 15px;">
                                                                <strong>
                                                                    <asp:Label ID="lblInputEmailTitle" runat="server" Font-Names="verdana" Font-Size="Small"></asp:Label></strong>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label" for="txtForgetEmail">
                                                                Email:</label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtForgetEmail" runat="server" CssClass="span12"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="btn btn-inverse"
                                                                    ValidationGroup="valgroupNewsLetterSignin" OnClientClick="return validateEmail();"
                                                                    OnClick="btnSubmit_Click" />
                                                                &nbsp;&nbsp;
                                                            <%--<a href="#" onclick="lnkBack_Click();" style="font-size: 85%;">< Back</a>--%>
                                                                <asp:LinkButton ID="lbtnBack" runat="server" Style="font-size: 85%;" OnClick="lbtnBack_Click"> < Back </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls" style="line-height: 15px;">
                                                                <asp:Label ID="lblEmailSubmitMsg" runat="server" Font-Names="verdana" Font-Size="Small"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hdn_pswd" runat="server" />
                                                    </asp:View>
                                                    <asp:View ID="v_UserForgotPwd_SecQstn" runat="server">
                                                        <div class="control-group">
                                                            <div class="controls" style="line-height: 15px;">
                                                                <strong style="font-family: Verdana; font-size: small;">Please answer below security
                                                                question</strong>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label" for="lblMail">
                                                                Email:
                                                            </label>
                                                            <div class="controls">
                                                                <asp:Label ID="lblMail" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label" for="lblSecQuestion">
                                                                Question:
                                                            </label>
                                                            <div class="controls">
                                                                <asp:Label ID="lblSecQuestion" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <label class="control-label" for="txtAnswer">
                                                                Enter Answer
                                                            </label>
                                                            <div class="controls">
                                                                <asp:TextBox ID="txtAnswer" runat="server" MaxLength="50" class="span12"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RfvtxtAns" runat="server" ControlToValidate="txtAnswer"
                                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <asp:HiddenField ID="hdfId" runat="server" />
                                                            <asp:HiddenField ID="hdfAns" runat="server" />
                                                            <div class="controls">
                                                                <asp:Label ID="lblAttempt" runat="server">You have maximum 5 attempts.</asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:Button ID="btnSubmitAnswer" runat="server" Text="Submit" CssClass="btn btn-inverse"
                                                                    OnClick="btnSubmitAnswer_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Names="verdana" Font-Size="Small"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="control-group">
                                                            <div class="controls">
                                                                <asp:CheckBox ID="chkResetPass" runat="server" Text="Do you want to reset password for your registered Email address ?"
                                                                    AutoPostBack="true" TextAlign="Left" Visible="false" OnCheckedChanged="chkResetPass_CheckedChanged" />
                                                            </div>
                                                        </div>
                                                    </asp:View>
                                                    <asp:View ID="v_UserForgotPwd_ResetPwd" runat="server">
                                                        RESET PASSWORD VIEW
                                                    </asp:View>
                                                </asp:MultiView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="span9">
                                <div class="row-fluid">
                                    <div class="span12">
                                        <span class="devzone">THE APP STORE</span>
                                        <p style="border: solid 0px #eee; padding-top: 5px; border-top: solid 3px #7BA43A;">
                                            A market place for financial Apps. Access hundreds of real time streaming financial
                                        applications in Interest Rate, Credit, FX, Weather and Equity derivatives asset
                                        classes on your desktop, excel, browsers, tablets or smartphones. Use them, share
                                        them. Or build your own and deploy those in weeks.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span12">
                                <p>
                                    Please contact us to request an access to THE BEAST APP STORE.
                                </p>
                                <p>
                                    Phone: +1-646-688-7500<br />
                                    Email: info@thebeastapps.com
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
                <!--Footer-->
                <div class="row-fluid footer">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span3">
                                <span>CONTACT US</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div>
                                            <br />
                                            <p style="color: #66AE1D;">
                                                NEW YORK
                                            </p>
                                            <p>
                                                The Beast Apps
                                            </p>
                                            <p>
                                                Email: <a href="#">info@thebeastapps.com</a>
                                            </p>
                                            <p style="color: #66AE1D;">
                                                INDIA
                                            </p>
                                            <p>
                                                The Beast Apps (India) Private Limited
                                            </p>
                                            <p>
                                                Email: <a href="#">infoindia@thebeastapps.com</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <span>Products &amp; Services</span>
                                <div class="row-fluid">
                                    <div class="span12" style="margin-left: 0px;">
                                        <div class="footer-link">
                                            <br />
                                            <p>
                                                <a href="#">THE BEAST APP STORE</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">PRIVATE APP STORE</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">TRADING & RISK MANAGEMENT
                                                SOLUTIONS</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">VOLMAX® Trading Solution</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">Risk Recycling® Solution</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">Weather Electronic Trading
                                                Platform</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span2">
                                <span>Others</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="footer-link">
                                            <br />
                                            <p>
                                                <a>Career</a>
                                            </p>
                                            <p>
                                                <a>Privacy</a>
                                            </p>
                                            <p>
                                                <a>Terms of Use</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <span>GET IN TOUCH</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="footer-link">
                                            <ul class="social_icons">
                                                <li><a href="#">
                                                    <img src="images/icon_social_fb.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_twitter.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_gplus.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_linked.png" border="0" /></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span12 footer-copyright">
                                <script type="text/javascript">
                                    copyright = new Date();
                                    update = copyright.getFullYear();
                                    document.write("Copyright © 2005-" + update + ". THE BEAST APPS. ALL RIGHTS RESERVED.");
                                </script>
                            </div>
                        </div>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
