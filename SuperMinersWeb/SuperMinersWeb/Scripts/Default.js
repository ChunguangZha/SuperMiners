
$().ready(function () {
    try{
        $("#form1").validate({
            rules: {
                txtTest : {
                    required: true,
                    minlength: 3,
                    maxlength: 15
                },
                MainContent_txtUserName: {
                    required: true,
                    minlength: 3,
                    maxlength: 15
                },
                //txtNickName: {
                //    maxlength: 15
                //},
                //MainContent_txtPassword: {
                //    required: true,
                //    minlength: 6,
                //    maxlength: 15
                //},
                //MainContent_txtConfirmPassword: {
                //    required: true,
                //    minlength: 6,
                //    maxlength: 15,
                //    equalTo: MainContent_txtPassword
                //},
                //MainContent_txtEmail: {
                //    required: true,
                //    maxlength: 20
                //}

            },
            messages: {
                txtTest: {
                    required: "请输入测试",
                    minlength: "测试最少3个字符",
                    maxlength: "测试最多15个字符"
                },
                MainContent_txtUserName: {
                    required: "请输入用户名",
                    minlength: "用户名最少3个字符",
                    maxlength: "用户名最多15个字符"
                },
                //txtNickName: {
                //    maxlength: "昵称最多15个字符"
                //},
                //MainContent_txtPassword: {
                //    required: "请输入密码",
                //    minlength: "密码最少6位",
                //    maxlength: "密码最多15位"
                //},
                //MainContent_txtConfirmPassword: {
                //    required: "请再次输入密码",
                //    minlength: "密码最少6位",
                //    maxlength: "密码最多15位",
                //    equalTo: "两次密码不一至，请重新输入"
                //},
                //MainContent_txtEmail: {
                //    required: "请输入电子邮箱",
                //    maxlength: "您输入的电子邮箱过长"
                //}
            }
        })
    } catch (err) {
        alert(err);
    }

    $("#MainContent_txtUserName").blur(CheckUserName);
});

function CheckUserName() {
    var username = $("#MainContent_txtUserName").val();
    if (username.length == 0) {
        $("#msgUserName").text("请输入用户名");
        return;
    }
    if (username.length < 3) {
        $("#msgUserName").text("用户名最少3个字符");
        return;
    }
    if (username.length > 15) {
        $("#msgUserName").text("用户名最多15个字符");
        return;
    }

    $.post("CheckUserName",
        { UserName: $("#MainContent_txtUserName").val() },
        function (data, status) {
            $("#msgUserName").text(data);
        });
}

function CallServerForUpdate() {
    document.getElementById("imgAuthCode").src = "AuthCode?Num=" + Math.random();
}
