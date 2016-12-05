
$(function () {

    //充值金币
    $("#abtnRechargeGoldCoin").on('click', function () {
        showInputNumberDialog("请输入充值金币数额", function (inputValue) {
            //var $xlUserName = $("#ContentPlaceHolder1_txtUserName").val();

            showLoadingToast();
            $.post("RechargeGoldCoin",
                { number: inputValue },
                function (data, status) {
                    closeLoadingToast();
                    if (data == "OK") {

                        //$.getJSON("RefreshXLUserInfo", function (result) {
                        $.get("RefreshXLUserInfo", function (data, status) {
                            if (data != "") {
                                var obj = eval("(" + data + ")");
                                if (obj != 'undefine') {
                                    $("#ContentPlaceHolder1_txtExp").val(obj.exp);
                                    $("#ContentPlaceHolder1_txtGoldCoin").val(obj.goldcoin);
                                    $("#ContentPlaceHolder1_txtMiners").val(obj.minerscount);
                                    $("#ContentPlaceHolder1_txtRMB").val(obj.rmb);
                                    $("#ContentPlaceHolder1_txtStones").val(obj.stockofstones);
                                    $("#ContentPlaceHolder1_txtWorkStonesReservers").val(obj.workstonesreservers);
                                    $("#ContentPlaceHolder1_txtLastGatherTime").val(obj.lastgathertime);

                                }
                            }
                        });
                        showOKToast();
                    }
                    else {
                        showMessageDialog(data);
                    }
                });
        });
    });



    $("#abtnGetTempStone").on('click', function () {
        showInputNumberDialog("正在收取矿石", function (inputValue) {
            var $txt = $("#ContentPlaceHolder1_txtLastGatherTime");

            $txt.val(inputValue);
        });
    });

});
