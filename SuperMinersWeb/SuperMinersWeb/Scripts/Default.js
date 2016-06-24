
$().ready(function () {
    $("#form1").validate({
        rules: {
            MainContent_txtUserName: {
                required: true,
                minlength: 3,
                maxlength: 15
            },
            MainContent_txtNickName: {
                maxlength: 15
            },
            MainContent_txtPassword: {
                required: true,
                minlength: 6,
                maxlength: 15
            },
            MainContent_txtConfirmPassword: {
                required: true,
                minlength: 6,
                maxlength: 15,
                equalTo: MainContent_txtPassword
            },
            MainContent_txtEmail: {
                required: true,
                maxlength: 20
            }

        },
        messages: {
            MainContent_txtUserName: {
                required: "请输入用户名",
                minlength: "用户名最少3个字符",
                maxlength: "用户名最多15个字符"
            },
            MainContent_txtNickName: {
                maxlength: "昵称最多15个字符"
            },
            MainContent_txtPassword: {
                required: "请输入密码",
                minlength: "密码最少6位",
                maxlength: "密码最多15位"
            },
            MainContent_txtConfirmPassword: {
                required: "请再次输入密码",
                minlength: "密码最少6位",
                maxlength: "密码最多15位",
                equalTo: "两次密码不一至，请重新输入"
            },
            MainContent_txtEmail: {
                required: "请输入电子邮箱",
                maxlength: "您输入的电子邮箱过长"
            }
        }
    });
});


function CallServerForUpdate() {
    document.getElementById("imgAuthCode").src = "AuthCode.ashx?Num=" + Math.random();
}
