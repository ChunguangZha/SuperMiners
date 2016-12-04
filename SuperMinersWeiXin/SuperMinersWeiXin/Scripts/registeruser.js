
var errorimgpath = "images/wxerror.png";
var okimgpath = "images/wxok.png";

$().ready(function () {

    $("#txtUserName").blur(CheckUserName);
    $("#txtNickName").blur(CheckNickName);
    $("#txtPassword").blur(CheckPassword);
    $("#txtConfirmPassword").blur(CheckConfirmPassword);
    $("#txtEmail").blur(CheckEmail);
    $("#txtQQ").blur(CheckQQ);
    $("#txtAuthCode").blur(CheckAuthCode);
    $("#txtAlipayAccount").blur(CheckAlipayAccount);
    $("#txtAlipayRealName").blur(CheckAlipayRealName);
    $("#txtIDCardNo").blur(CheckIDCardNo);

    $("#imgAuthCode").on('click', function () {
        $(this).attr("src", "AuthCode?Num=" + Math.random());
        $("#txtAuthCode").text("");
        $("#imgAuthCodeOK").css("display", "none");

    });
    $("#weuiAgree").click(function () {
        var $checked = $(this).is(':checked');
        showAlertMessage($checked);
        var $btn = $("#btnRegister");
        if ($checked == true) {
            $btn.removeAttr("disabled");
            $btn.removeClass("weui-btn_disabled");
        } else {
            $btn.attr("disabled", 'disabled');
            $btn.addClass("weui-btn_disabled");
        }
    });
});

function CheckUserName() {
    var $imgOK = $("#imgUserNameOK");
    $imgOK.css("display", "none");

    var username = $("#txtUserName").val();
    if (username.length == 0) {
        return;
    }
    if (username.length < 3) {
        $imgOK.attr("src", errorimgpath);
        $imgOK.css("display", "inline");
        showAlertMessage("用户名最少3个字符");
        return;
    }

    $.post("CheckUserName",
        { UserName: username },
        function (data, status) {
            if (data == "OK") {
                $imgOK.attr("src", okimgpath);
            }
            else {
                $imgOK.attr("src", errorimgpath);
                showAlertMessage(data);
            }
            $imgOK.css("display", "inline");
        });
}
function CheckNickName() {
    //$("#msgPassword").text("");
    //var nickname = $("#MainContent_txtNickName").val();
    //if (nickname.length == 0) {
    //    $("#msgNickName").text("请输入昵称");
    //    return;
    //}

}

function CheckPassword() {
    var $imgOK = $("#imgPasswordOK");
    $imgOK.css("display", "none");

    var pwd = $("#txtPassword").val();
    if (pwd.length == 0) {
        return;
    }
    if (pwd.length < 6) {
        $imgOK.attr("src", errorimgpath);
        showAlertMessage("密码至少6个字符");
    } else {
        $imgOK.attr("src", okimgpath);
    }

    $imgOK.css("display", "inline");

}
function CheckConfirmPassword() {
    var $imgOK = $("#imgCpasswordOK");
    $imgOK.css("display", "none");

    var pwd = $("#txtPassword").val();
    var confirmpwd = $("#txtConfirmPassword").val();
    if (confirmpwd.length == 0) {
        return;
    }
    if (pwd != confirmpwd) {
        $imgOK.attr("src", errorimgpath);
        showAlertMessage("两次密码不一至，请重新输入");
    } else {
        $imgOK.attr("src", okimgpath);
    }

    $imgOK.css("display", "inline");
}

function CheckAlipayAccount() {
    var $imgOK = $("#imgAlipayAccountOK");
    $imgOK.css("display", "none");

    var alipayAccount = $("#txtAlipayAccount").val();
    if (alipayAccount.length == 0) {
        return;
    }

    var szReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    var bChk = szReg.test(alipayAccount);
    if (!bChk) {
        szReg = /^([1-9][0-9]*)$/;
        if (!szReg.test(alipayAccount) || alipayAccount.length != 11) {
            $imgOK.attr("src", errorimgpath);
            $imgOK.css("display", "inline");
            showAlertMessage("支付宝账户格式错误");
            return;
        }
    }

    $.post("CheckAlipayAccount",
        { AlipayAccount: alipayAccount },
        function (data, status) {
            if (data == "OK") {
                $imgOK.attr("src", okimgpath);
            } else {
                $imgOK.attr("src", errorimgpath);
                showAlertMessage(data);
            }
            $imgOK.css("display", "inline");
        });

}

function CheckAlipayRealName() {
    var imgOK = $("#imgAlipayRealNameOK");
    imgOK.css("display", "none");

    var alipayRealName = $("#txtAlipayRealName").val();
    if (alipayRealName.length == 0) {
        return;
    }
    var szReg = /^[\u4E00-\u9FA5\uF900-\uFA2D]/;
    var bChk = szReg.test(alipayRealName);
    if (!bChk) {
        $imgOK.attr("src", errorimgpath);
        showAlertMessage("支付宝实名错误");
    }
    else {
        $imgOK.attr("src", okimgpath);
    }

    $imgOK.css("display", "inline");

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
    var $imgOK = $("#imgIDCardNoOK");
    $imgOK.css("display", "none");

    var IDCardNo = $("#txtIDCardNo").val();
    if (IDCardNo.length == 0) {
        return;
    }
    var szReg = /^([1-9][0-9]*X{0,1})$/;
    if (!szReg.test(IDCardNo) || IDCardNo.length != 18) {
        $imgOK.attr("src", errorimgpath);
        $imgOK.css("display", "inline");
        showAlertMessage("身份证号格式错误");
        return;
    }
    $.post("CheckIDCardNo",
        { IDCardNo: IDCardNo },
        function (data, status) {
            if (data == "OK") {
                $imgOK.attr("src", okimgpath);
            } else {
                $imgOK.attr("src", errorimgpath);
                showAlertMessage(data);
            }
            $imgOK.css("display", "inline");
        });

}

function CheckQQ() {
    var $imgOK = $("#imgQQOK");
    $imgOK.css("display", "none");

    var qq = $("#txtQQ").val();
    if (qq.length == 0) {
        return;
    }
    var szReg = /^([1-9][0-9]*)$/;
    if (!szReg.test(qq)) {
        $imgOK.attr("src", errorimgpath);
        showAlertMessage("QQ号格式错误");
    }
    else {
        $imgOK.attr("src", okimgpath);
    }
    $imgOK.css("display", "inline");
}

function CheckEmail() {
    var $imgOK = $("#imgEmailOK");
    $imgOK.css("display", "none");

    var email = $("#txtEmail").val();
    if (email.length == 0) {
        return;
    }

    var szReg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
    var bChk = szReg.test(email);
    if (!bChk) {
        $imgOK.attr("src", errorimgpath);
        $imgOK.css("display", "inline");
        showAlertMessage("邮箱格式错误");
        return;
    }

    $.post("CheckEmail",
        { Email: email },
        function (data, status) {
            if (data == "OK") {
                $imgOK.attr("src", okimgpath);
            } else {
                $imgOK.attr("src", errorimgpath);
                showAlertMessage(data);
            }
            $imgOK.css("display", "inline");
        });
}

function CheckAuthCode() {
    var $imgOK = $("#imgAuthCodeOK");
    $imgOK.css("display", "none");

    var authcode = $("#txtAuthCode").val();

    $.post("CheckAuthCode",
        { AuthCode: authcode },
        function (data, status) {
            if (data == "OK") {
                $imgOK.attr("src", okimgpath);
            } else {
                $imgOK.attr("src", errorimgpath);
                showAlertMessage(data);
            }
            $imgOK.css("display", "inline");
        });
}


function showAlertMessage(message){
    var $tooltips = $('.js_tooltips');
    $tooltips.text(message);

    if ($tooltips.css('display') != 'none') return;

    // toptips的fixed, 如果有`animation`, `position: fixed`不生效
    $('.page.cell').removeClass('slideIn');

    $tooltips.css('display', 'block');
    setTimeout(function () {
        $tooltips.css('display', 'none');
    }, 2000);
}

