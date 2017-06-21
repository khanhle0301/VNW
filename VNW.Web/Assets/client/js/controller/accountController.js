var accountController = {
    init: function () {
        accountController.registerEvents();
    },
    registerEvents: function () {

        $('#frmSignUp').validate({
            rules: {
                username: "required",
                password: "required"
            },
            messages: {
                username: "Nhập tài khoản",
                password: "Nhập mật khẩu"
            }
        });


        $('#form_sign_in').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmSignUp').valid();
            if (isValid) {
                accountController.createLogin();
            }
        });

        $('#frmSignOut').validate({
            rules: {
                txtLastname: "required",
                txtFirstname: "required",
                txtEmail: {
                    required: true,
                    email: true
                },
                txtPassword: "required"
            },
            messages: {
                txtLastname: "Nhập họ",
                txtFirstname: "Nhập tên",
                password: "Nhập mật khẩu",
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                }
            }
        });

        $('#form_sign_up').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#frmSignOut').valid();
            if (isValid) {
                accountController.register();
            }
        });

    },

    createLogin: function () {
        var username = $('#form_username').val();
        var passWord = $('#form_password').val();
        var returnUrl = $('#returnUrl').val();
        $.ajax({
            url: '/Account/Login',
            data: {
                username: username,
                password: passWord,
                returnUrl: returnUrl
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    window.location.href = response.returnUrl;
                }
                else {
                    setTimeout(function () {
                        $('#error_message').text(response.message);
                        $('#err_top').show();
                    });
                }
            }
        });
    },

    register: function () {
        var nhanmail = false;
        if ($("#chkIsNewLetter").is(':checked')) {
            nhanmail = true;
        }

        var register = {
            FullName: $('#txtLastname').val() + ' ' + $('#txtFirstname').val(),
            UserName: $('#txtEmail').val(),
            Password: $('#txtPassword').val(),
            Email: $('#txtEmail').val(),
            PhoneNumber: '0909090909',
            NhanMail: nhanmail
        }
        $.ajax({
            url: '/Account/Register',
            type: 'POST',
            dataType: 'json',
            data: {
                accountViewModel: JSON.stringify(register)
            },
            success: function (response) {

                if (response.status) {
                    alert('Đăng ký thành công.');
                    window.location.href = "/";
                }
                else {
                    setTimeout(function () {
                        $('#error_message').text(response.message);
                        $('#err_top').show();
                    });
                }
            }
        });
    }
}
accountController.init();