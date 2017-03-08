
$().ready(function () {
    //123
    $("#MainContent_txtUserName").blur(CheckUserName);
    $("#MainContent_txtPassword").blur(CheckPassword);
    $("#MainContent_txtEmail").blur(CheckEmail);
    $("#MainContent_txtAuthCode").blur(CheckAuthCode);
    $("#MainContent_txtAlipayAccount").blur(CheckAlipayAccount);
    $("#MainContent_txtAlipayRealName").blur(CheckAlipayRealName);
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
}

//function CheckNewServerUserLoginName() {

//    $("#msgNewServerUserLoginName").text("");
//    $("#imgNewServerUserLoginNameOK").css("display", "none");

//    var newServerUserLoginName = $("#MainContent_txtNewServerUserLoginName").val();
//    if (newServerUserLoginName.length == 0) {
//        $("#msgNewServerUserLoginName").text("请输入新区用户名");
//        return;
//    }
//}

//function CheckNewServerUserLoginName() {

//    $("#msgNewServerPassword").text("");
//    $("#imgNewServerPasswordOK").css("display", "none");

//    var newServerPassword = $("#MainContent_txtNewServerPassword").val();
//    if (newServerPassword.length == 0) {
//        $("#msgNewServerPassword").text("请输入新区密码");
//        return;
//    }
//}

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
