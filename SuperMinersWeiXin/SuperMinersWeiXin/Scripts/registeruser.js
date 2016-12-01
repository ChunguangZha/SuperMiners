
$().ready(function () {
    try {
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
    $("#MainContent_txtNickName").blur(CheckNickName);
    $("#MainContent_txtPassword").blur(CheckPassword);
    $("#MainContent_txtConfirmPassword").blur(CheckConfirmPassword);
    $("#MainContent_txtEmail").blur(CheckEmail);
    $("#MainContent_txtQQ").blur(CheckQQ);
    $("#MainContent_txtAuthCode").blur(CheckAuthCode);
    $("#MainContent_txtAlipayAccount").blur(CheckAlipayAccount);
    $("#MainContent_txtAlipayRealName").blur(CheckAlipayRealName);
    $("#MainContent_txtIDCardNo").blur(CheckIDCardNo);
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

    $.post("http://www.xlore.net/CheckUserName",
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
    $("#msgPassword").text("");
    var nickname = $("#MainContent_txtNickName").val();
    if (nickname.length == 0) {
        $("#msgNickName").text("请输入昵称");
        return;
    }

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

function CheckAlipayAccount() {
    $("#msgAlipayAccount").text("");
    $("#imgAlipayAccountOK").css("display", "none");

    var alipayAccount = $("#MainContent_txtAlipayAccount").val();
    if (alipayAccount.length == 0) {
        $("#msgAlipayAccount").text("请输入支付宝账户");
        return;
    }

    var szReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    var bChk = szReg.test(alipayAccount);
    if (!bChk) {
        szReg = /^([1-9][0-9]*)$/;
        if (!szReg.test(alipayAccount)) {
            $("#msgAlipayAccount").text("支付宝账户只能为电子邮箱或者手机号");
            return;
        } else {
            if (alipayAccount.length != 11) {
                $("#msgAlipayAccount").text("支付宝账户只能为电子邮箱或者手机号");
                return;
            }
        }
    }

    $.post("http://www.xlore.net/CheckAlipayAccount",
        { AlipayAccount: alipayAccount },
        function (data, status) {
            if (data == "OK") {
                $("#imgAlipayAccountOK").css("display", "inline");
            } else {
                $("#msgAlipayAccount").text(data);
            }
        });

}

function CheckAlipayRealName() {
    $("#msgAlipayRealName").text("");
    $("#imgAlipayRealNameOK").css("display", "none");

    var alipayRealName = $("#MainContent_txtAlipayRealName").val();
    if (alipayRealName.length == 0) {
        $("#msgAlipayRealName").text("请输入支付宝实名");
        return;
    }
    var szReg = /^[\u4E00-\u9FA5\uF900-\uFA2D]/;
    var bChk = szReg.test(alipayRealName);
    if (!bChk) {
        $("#msgAlipayRealName").text("请输入正确支付宝实名");
        return;
    }

    $("#imgAlipayRealNameOK").css("display", "inline");
    //$.post("CheckAlipayRealName",
    //    { AlipayRealName: alipayRealName },
    //    function (data, status) {
    //        if (data == "OK") {
    //            $("#imgAlipayRealNameOK").css("display", "inline");
    //        } else {
    //            $("#msgAlipayRealName").text(data);
    //        }
    //    });

}

function CheckIDCardNo() {
    $("#msgIDCardNo").text("");
    $("#imgIDCardNoOK").css("display", "none");

    var IDCardNo = $("#MainContent_txtIDCardNo").val();
    if (IDCardNo.length == 0) {
        $("#msgIDCardNo").text("请输入身份证号");
        return;
    }
    var szReg = /^([1-9][0-9]*X{0,1})$/;
    if (!szReg.test(IDCardNo)) {
        $("#msgIDCardNo").text("请输入正确的身份证号");
        return;
    }
    if (IDCardNo.length != 18) {
        $("#msgIDCardNo").text("身份证号必须为18位");
        return;
    }
    $.post("http://www.xlore.net/CheckIDCardNo",
        { IDCardNo: IDCardNo },
        function (data, status) {
            if (data == "OK") {
                $("#imgIDCardNoOK").css("display", "inline");
            } else {
                $("#imgIDCardNoOK").css("display", "none");
                $("#msgIDCardNo").text(data);
            }
        });

    //$("#imgIDCardNoOK").css("display", "inline");
}

function CheckQQ() {
    $("#msgQQ").text("");
    $("#imgQQOK").css("display", "none");

    var qq = $("#MainContent_txtQQ").val();
    if (qq.length == 0) {
        $("#msgQQ").text("请输入QQ");
        return;
    }
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

    $.post("http://www.xlore.net/CheckEmail",
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

    $.post("http://www.xlore.net/CheckAuthCode",
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
