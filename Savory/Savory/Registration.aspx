<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="Savory.Registration" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <input type="button" id="createAccount" value="Create Account" class="btn btn-sm btn-primary" data-toggle="modal" data-target="#myModal" />
    
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="firstname" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Last Name</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Last Name" ID="lastname"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="lastname" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Email Id</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Email Id" ID="emailid"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="emailid" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="emailid" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Password</label>
                        <asp:TextBox class="form-control input-sm" runat="server" TextMode="Password" placeholder="Enter Password" ID="pwd"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="pwd" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label"><strong style="color: red">*</strong> Phone Number</label>
                        <asp:TextBox class="form-control input-sm" runat="server" placeholder="Enter Phone Number" ID="phonenumber"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="phonenumber" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="phonenumber" ForeColor="Red" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="month" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="day" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="aniMonth" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="a" runat="server" ErrorMessage="*" ControlToValidate="aniDay" ForeColor="Red"></asp:RequiredFieldValidator>
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
                        <asp:Button ID="btn_createAccount" CssClass="btn btn-primary" runat="server" ValidationGroup="a" OnClick="btn_createAccount_Click" Text="Create Account" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--<script src="//ajax.aspnetcdn.com/ajax/jquery.validate/1.9/jquery.validate.min.js"></script>--%>
    <!-- jQuery Form Validation code -->
    <script>
        $(function () {

            // Setup form validation on the #register-form element
            $("#form1").validate({

                // Specify the validation rules
                rules: {
                    firstname: "required",
                    lastname: "required",
                    emailid: {
                        required: true,
                        email: true
                    },
                    pwd: {
                        required: true,
                        minlength: 6
                    },
                    phonenumber: {
                        required: true,
                        number: true,
                        minlength: 10
                    },
                    mobilenumber: {
                        number: true,
                        minlength: 10
                    },
                    month: {
                        required: true
                    },
                    day: {
                        required: true
                    },
                    radioconform: {
                        required: true
                    }
                },

                // Specify the validation error messages
                messages: {
                    firstname: "Please enter your First Name",
                    lastname: "Please enter your Last Name",
                    pwd: {
                        required: "Please provide a Password",
                        minlength: "Your password must be at least 6 characters long"
                    },
                    emailid: {
                        required: "Please enter your Email Id",
                        email: "Please enter a valid Email Id"
                    },
                    phonenumber: {
                        required: "Please enter your Phone Number",
                        number: "Please Enter Number Only",
                        minlength: "Enter Phone Number with Area Code"
                    },
                    mobilenumber: {
                        number: "Please Enter Number Only",
                        minlength: "Enter Valid Mobile Number"
                    },
                    month: "Please Select Month",
                    day: "Please Select Day",
                    radioconform: "Please Select One Option"
                },

                submitHandler: function (form) {
                    form.submit();
                }
            });

        });
    </script>
    <style>
        .error
        {
            color: red;
        }
    </style>

</asp:Content>
