
$().ready(function () {
    try {
        alert("start");
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
                MainContent_txtEmail: {
                    required: "请输入电子邮箱",
                    maxlength: "您输入的电子邮箱过长"
                }
            }
        })
    } catch (err) {
        //alert(err);
    }

    //123
    $("#MainContent_txtUserName").blur(CheckUserName);
    $("#MainContent_txtPassword").blur(CheckPassword);
    $("#MainContent_txtConfirmPassword").blur(CheckConfirmPassword);
    $("#MainContent_txtEmail").blur(CheckEmail);
    $("#MainContent_txtQQ").blur(CheckQQ);
    $("#MainContent_txtAuthCode").blur(CheckAuthCode);
});

function CheckUserName() {
    $("#msgUserName").text("");
    $("#imgUserNameOK").css("display", "none");

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
            if (data == "OK") {
                $("#imgUserNameOK").css("display", "inline");
            }
            else {
                $("#msgUserName").text(data);
            }
        });
}
function CheckNickName() {
    var nickname = $("#MainContent_txtNickName").val();

}
function CheckPassword() {
    $("#msgPassword").text("");
    $("#imgPasswordOK").css("display", "none");

    var pwd = $("#MainContent_txtPassword").val();
    if (pwd.length < 6) {
        $("#msgPassword").text("密码至少6个字符");
        return;
    }

    $("#imgPasswordOK").css("display", "inline");
}
function CheckConfirmPassword() {
    $("#msgConfirmPassword").text("");
    $("#imgConfirmPasswordOK").css("display", "none");

    var pwd = $("#MainContent_txtPassword").val();
    var confirmpwd = $("#MainContent_txtConfirmPassword").val();
    if (pwd != confirmpwd) {
        $("#msgConfirmPassword").text("两次密码不一至，请重新输入");
        return;
    }

    $("#imgConfirmPasswordOK").css("display", "inline");
}
function CheckQQ() {
    $("#msgQQ").text("");
    $("#imgQQOK").css("display", "none");

    var qq = $("#MainContent_txtQQ").val();
    var szReg = /^([1-9][0-9]*)$/;
    if (!szReg.test(qq)) {
        $("#msgQQ").text("QQ账户只能输入数字");
        return;
    }

    $("#imgQQOK").css("display", "inline");
}
function CheckEmail() {
    $("#msgEmail").text("");
    $("#imgEmailOK").css("display", "none");

    var email = $("#MainContent_txtEmail").val();
    if (email.length == 0) {
        $("#msgEmail").text("请输入邮箱");
        return;
    }

    var szReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    var bChk = szReg.test(email);
    if (!bChk) {
        $("#msgEmail").text("请输入正确邮箱地址");
        return;
    }

    $.post("CheckEmail",
        { Email: email },
        function (data, status) {
            if (data == "OK") {
                $("#imgEmailOK").css("display", "inline");
            } else {
                $("#msgEmail").text(data);
            }
        });
}
function CheckAuthCode() {
    $("#msgAuthCode").text("");
    $("#imgAuthCodeOK").css("display", "none");

    var authcode = $("#MainContent_txtAuthCode").val();

    $.post("CheckAuthCode",
        { AuthCode: authcode },
        function (data, status) {
            if (data == "OK") {
                $("#imgAuthCodeOK").css("display", "inline");
            } else {
                $("#msgAuthCode").text(data);
            }
        });
}

function CallServerForUpdate() {
    //document.getElementById("imgAuthCode").src = "AuthCode?Num=" + Math.random();
    $("#imgAuthCode").attr("src", "AuthCode?Num=" + Math.random());
    $("#msgAuthCode").text("");
    $("#imgAuthCodeOK").css("display", "none");

}
