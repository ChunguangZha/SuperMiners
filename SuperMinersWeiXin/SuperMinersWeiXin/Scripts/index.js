
$(function () {

    //充值金币
    $("#abtnRechargeGoldCoin").on('click', function () {
        showInputNumberDialog(
            "请输入充值金币数额",
            "最少为100",
            function (inputValue) {
                showLoadingToast();
                $.post("RechargeGoldCoin", { number: inputValue }, callHandlerCallback);
            },
            function (inputValue) {
                return inputValue >= 100;
            }
        );
    });

    //勘探矿山
    $("#abtnBuyStoneReservers").on('click', function () {
        showConfirmDialog("请确认", "勘探一座矿山将花费3000灵币", "确认", "取消", function () {

            showLoadingToast();
            $.post("BuyMineHandler", {}, callHandlerCallback);
        });
    });

    //购买矿工
    $("#abtnBuyMiner").on('click', function () {
        showInputNumberDialog(
            "请输入购买矿工数量",
            "",
            function (inputValue) {
                showLoadingToast();
                $.post("BuyMinerHandler", { number: inputValue }, callHandlerCallback);
            },
            function (inputValue) {
                return inputValue < 5000;
            });
    });

    //出售矿石
    $("#abtnSellStone").on('click', function () {
        showInputNumberDialog(
            "请输入出售矿石数量",
            "最少为1000",
            function (inputValue) {
                showLoadingToast();
                $.post("SellStoneHandler", { number: inputValue }, callHandlerCallback);
            },
            function (inputValue) {
                return inputValue >= 1000;
            }
        );
    });
    
    //收取矿石
    $("#abtnGetTempStone").on('click', function () {
        showLoadingToast();
        $.post("GatherStoneHandler", {}, function (data, status) {
            closeLoadingToast();

            if (data.length < 3) {
                showMessageDialog(data);
                return;
            }
            
            var dataHeader = data.substring(0, 2);
            if (dataHeader == "OK") {
                var dataContent = data.substring(3, data.length);
                refreshUserInfo();
                showMessageDialog('成功收取' + dataContent + '块矿石');
            }
            else {
                showMessageDialog(data);
            }
        });
    });

});

function callHandlerCallback (data, status) {
    closeLoadingToast();
    if (data == "OK") {
        refreshUserInfo();
        showOKToast();
    }
    else {
        showMessageDialog(data);
    }
}

function refreshUserInfo() {

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
}
