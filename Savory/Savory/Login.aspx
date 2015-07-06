<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Savory.Index" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="bs-docs-section" id="dvUserLogin">
        <div class="row">
            <div class="col-md-5">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Login</h3>
                    </div>
                    <div class="panel-body">
                        <asp:UpdatePanel ID="upnlUserLogin" runat="server">
                            <ContentTemplate>
                                <asp:MultiView ID="mvUserLogin" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="v_UserLogin" runat="server" OnActivate="v_UserLogin_Activate">
                                        <div class="form-group">
                                            <label class="control-label" for="txtSignInUserID">Email</label>
                                            <asp:TextBox ID="txtSignInUserID" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                ToolTip="User ID (e.g. userid@thebeastapps.com)" placeholder="Email" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfielduid" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                Display="None" ControlToValidate="txtSignInUserID" ErrorMessage="Please enter email id"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="valgroupNewsLetterSignin"
                                                runat="server" Display="None" ControlToValidate="txtSignInUserID" ToolTip="Enter Valid Email Id"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Please enter valid email id"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label" for="txtSignInUserPass">Password</label>
                                            <asp:TextBox ID="txtSignInUserPass" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                TextMode="Password" ToolTip="Password" placeholder="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfieldpwd" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                Display="None" ControlToValidate="txtSignInUserPass" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSignIn" runat="server" Text="Sign in" CssClass="btn btn-sm btn-primary"
                                                OnClick="btnSignIn_Click" ValidationGroup="valgroupNewsLetterSignin" />
                                            <asp:ValidationSummary ID="vsSign" runat="server" ValidationGroup="valgroupNewsLetterSignin"
                                                ShowMessageBox="true" ShowSummary="false" />
                                            &nbsp;&nbsp;<%-- <a href="#" onclick="forgotPwd_Click();" style="font-size: 85%;">Forgot
                                                                Password?</a>--%><asp:LinkButton ID="lbtnForgotPwd" runat="server" Text="Forgot Password ?"
                                                                    Style="font-size: 85%;" OnClick="lbtnForgotPwd_Click"></asp:LinkButton>
                                        </div>
                                        <div class="form-group">
                                            <div style="line-height: 15px;">
                                                <asp:Label ID="lblSigninMsg" ForeColor="Red" runat="server" Font-Names="verdana"
                                                    Font-Size="Small"></asp:Label>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="v_UserForgotPwd_EmailInput" runat="server" OnActivate="v_UserForgotPwd_EmailInput_Activate">
                                        <%--Enter Your Registered Email Address to Receive Your Password--%>
                                        <div class="form-group">
                                            <div style="line-height: 15px;">
                                                <strong>
                                                    <asp:Label ID="lblInputEmailTitle" runat="server" Font-Names="verdana" Font-Size="Small"></asp:Label></strong>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="control-label" for="txtForgetEmail">
                                                Email:</label>
                                            <asp:TextBox ID="txtForgetEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" CssClass="btn btn-sm btn-primary"
                                                ValidationGroup="valgroupNewsLetterSignin" OnClientClick="return validateEmail();"
                                                OnClick="btnSubmit_Click" />
                                            &nbsp;&nbsp;
                                                            <%--<a href="#" onclick="lnkBack_Click();" style="font-size: 85%;">< Back</a>--%>
                                            <asp:LinkButton ID="lbtnBack" runat="server" Style="font-size: 85%;" OnClick="lbtnBack_Click"> < Back </asp:LinkButton>
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
            <div class="col-md-2" style="text-align: center;">
                <img src="Images/login_or.png" />
            </div>
            <div class="col-md-5">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">First time?</h3>
                    </div>
                    <div class="panel-body">
                        <p style="text-align: justify">
                            Create an Account to make future purchases speedier in the future. You can Re-Order with just one click and receive future coupons from us.
                        </p>
                        <input type="button" id="createAccount" value="Create Account" class="btn btn-sm btn-primary" data-toggle="modal" data-target="#myModal" />
                        <p style="text-align: justify">
                            <br />
                            Want to setup your account later? No problem. Select "Order as a Guest" to proceed to Checkout.
                        </p>
                        <asp:Button ID="orderAsGuest" runat="server" Text="Order as a Guest" CssClass="btn btn-sm btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                    <h4 class="modal-title" id="myModalLabel">Create Account</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label">Fields marked with a <strong style="color: red">*</strong> are required.</label>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> First Name</label>
                        <asp:TextBox ID="firstname" CssClass="form-control input-sm" placeholder="Enter First Name" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" runat="server" ErrorMessage="Please enter your First Name" ControlToValidate="firstname" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Last Name</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Last Name" ID="lastname"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" runat="server" ErrorMessage="Please enter your Last Name" ControlToValidate="lastname" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Email Id</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Email Id" ID="emailid"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" runat="server" ErrorMessage="Please enter your Email Id" ControlToValidate="emailid" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" ErrorMessage="Please enter Valid Email Id" ControlToValidate="emailid" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Password</label>
                        <asp:TextBox class="form-control input-sm" runat="server" TextMode="Password" placeholder="Enter Password" ID="pwd"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" runat="server" ErrorMessage="Please provide your Password" ControlToValidate="pwd" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Phone Number</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Phone Number" ID="phonenumber"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" runat="server" ErrorMessage="Please enter your Phone Number" ControlToValidate="phonenumber" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" ErrorMessage="Please enter Phone Number with Area Code" ControlToValidate="phonenumber" ForeColor="Red" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">Mobile Number</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Mobile Number" ID="mobilenumber" MaxLength="10"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Birthday</label>
                        <asp:DropDownList ID="month" CssClass="form-control input-sm" runat="server">
                            <asp:ListItem Value="" Text="--Select Month--"></asp:ListItem>
                            <asp:ListItem Value="01" Text="January"></asp:ListItem>
                            <asp:ListItem Value="02" Text="February"></asp:ListItem>
                            <asp:ListItem Value="03" Text="March"></asp:ListItem>
                            <asp:ListItem Value="04" Text="April"></asp:ListItem>
                            <asp:ListItem Value="05" Text="May"></asp:ListItem>
                            <asp:ListItem Value="06" Text="June"></asp:ListItem>
                            <asp:ListItem Value="07" Text="July"></asp:ListItem>
                            <asp:ListItem Value="08" Text="August"></asp:ListItem>
                            <asp:ListItem Value="09" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" runat="server" ErrorMessage="Please Select Month" ControlToValidate="month" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="day" CssClass="form-control input-sm" runat="server">
                            <asp:ListItem Value="" Text="--Day--"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a" runat="server" ErrorMessage="Please Select Day" ControlToValidate="day" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">Anniversary</label>
                        <asp:DropDownList ID="aniMonth" CssClass="form-control input-sm" runat="server">
                            <asp:ListItem Value="" Text="--Select Month--"></asp:ListItem>
                            <asp:ListItem Value="01" Text="January"></asp:ListItem>
                            <asp:ListItem Value="02" Text="February"></asp:ListItem>
                            <asp:ListItem Value="03" Text="March"></asp:ListItem>
                            <asp:ListItem Value="04" Text="April"></asp:ListItem>
                            <asp:ListItem Value="05" Text="May"></asp:ListItem>
                            <asp:ListItem Value="06" Text="June"></asp:ListItem>
                            <asp:ListItem Value="07" Text="July"></asp:ListItem>
                            <asp:ListItem Value="08" Text="August"></asp:ListItem>
                            <asp:ListItem Value="09" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="aniDay" CssClass="form-control input-sm" runat="server">
                            <asp:ListItem Value="" Text="--Day--"></asp:ListItem>
                            <asp:ListItem Value="01" Text="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Have you ever ordered from us before (in-store or online)? </label>
                        <asp:RadioButtonList ID="radioconform" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" Selected="True" />
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <asp:CheckBox CssClass="checkbox-inline" Text="Email me specials" Checked="true" runat="server" ID="checkEmail" />
                        <asp:CheckBox Text="Text me specials" CssClass="checkbox-inline" Checked="true" runat="server" ID="checkText" />

                    </div>
                    <div class="form-group">
                        <asp:Button ID="btn_createAccount" CssClass="btn btn-primary" runat="server" ValidationGroup="a" Text="Create Account" OnClick="createAccount_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
