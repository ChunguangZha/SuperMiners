
$().ready(function () {

    //123
    $("#MainContent_txtUserLoginName").blur(CheckUserName);
    $("#MainContent_txtAuthCode").blur(CheckAuthCode);
});

function CheckUserName() {
    $("#msgUserName").text("");
    $("#imgUserNameOK").css("display", "none");

    var username = $("#MainContent_txtUserLoginName").val();
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
