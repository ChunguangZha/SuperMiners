
$(function () {

    var $pageitemcount = 10;
    var $pageindex_goldcoinrecords = 0;
    var $pageindex_minerecords = 0;
    var $pageindex_minerrecords = 0;
    var $pageindex_rmbrechargerecords = 0;
    var $pageindex_rmbwithdrawrecords = 0;
    var $pageindex_stonebuyrecords = 0;
    var $pageindex_stonesellrecords = 0;

    var $topnav = $("#topnav");

    var $current_nav = $topnav.children(".weui-bar__item_on");
    var $current_id, $current_listview;
    var $divloading = $("#divloading");
    var $btnloadmore = $("#btnloadmore");

    function changeCurrentListView() {
        $current_id = $current_nav.attr("id");
        $current_listview = $("div[data-id='" + $current_id + "']");
        $current_listview.css("display", "block").siblings(".listrecords").css("display", "none");

        loadRecords();
    }

    changeCurrentListView();

    $('.weui-navbar__item').on('click', function () {
        $current_nav = $(this);
        $current_nav.addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
        
        changeCurrentListView();

    });
    $btnloadmore.on('click', loadRecords);

    function loadRecords() {

        if ($current_id == "navgoldcoinrecords") {
            getGoldCoinRecordList_Simple();
        } else if ($current_id == "navminerecords") {
            getMineRecordList_Simple();
        } else if ($current_id == "navminerrecords") {
            getMinerRecordList_Simple();
        } else if ($current_id == "navrmbwithdrawrecords") {
            getWithdrawRMBRecordList_Simple();
        } else if ($current_id == "navstonebuyrecords") {
            getUserBuyStoneRecordList_Simple();
        } else if ($current_id == "navstonesellrecords") {
            getUserSellStoneRecordList_Simple();
        }
    }

    function getGoldCoinRecordList_Simple() {

        var $listviewGoldCoinRecords = $("div[data-id='navgoldcoinrecords']");

        getRecordList("AsyncGetGoldCoinBuyRecordHandler", $listviewGoldCoinRecords, $pageindex_goldcoinrecords, function (records, pageindex) {

            $pageindex_goldcoinrecords = pageindex;
            var html = '';
            $(records).each(function () {
                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_goldcoinrecords,
                    time: this.MyCreateTime.Year + '/' + this.MyCreateTime.Month + '/' + this.MyCreateTime.Day + ' ' + this.MyCreateTime.Hour + ':' + this.MyCreateTime.Minute + ':' + this.MyCreateTime.Second,
                    count: '充值金币: ' + this.GainGoldCoin,
                });
                html += rowhtml;
            });

            return html;
        });
    };

    function getMineRecordList_Simple() {

        var $listviewMineRecords = $("div[data-id='navminerecords']");
        getRecordList("AsyncGetMineBuyRecordHandler", $listviewMineRecords, $pageindex_minerecords, function (records, pageindex) {

            $pageindex_minerecords = pageindex;
            var html = '';
            $(records).each(function () {
                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_minerecords,
                    time: this.MyCreateTime.Year + '/' + this.MyCreateTime.Month + '/' + this.MyCreateTime.Day + ' ' + this.MyCreateTime.Hour + ':' + this.MyCreateTime.Minute + ':' + this.MyCreateTime.Second,
                    count: '获取储量: ' + this.GainStonesReserves,
                });
                html += rowhtml;
            });

            return html;
        });
    }

    function getMinerRecordList_Simple() {

        var $listviewMinerRecords = $("div[data-id='navminerrecords']");

        getRecordList("AsyncGetMinerBuyRecordHandler", $listviewMinerRecords, $pageindex_minerrecords, function (records, pageindex) {

            $pageindex_minerrecords = pageindex;
            var html = '';
            $(records).each(function () {
                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_minerrecords,
                    time: this.MyTime.Year + '/' + this.MyTime.Month + '/' + this.MyTime.Day + ' ' + this.MyTime.Hour + ':' + this.MyTime.Minute + ':' + this.MyTime.Second,
                    count: '购买矿工: ' + this.GainMinersCount,
                });
                html += rowhtml;
            });

            return html;
        });
    };

    function getWithdrawRMBRecordList_Simple() {

        var $listviewWithdrawRMBRecords = $("div[data-id='navrmbwithdrawrecords']");

        getRecordList("AsyncGetWithdrawRMBRecordListHandler", $listviewWithdrawRMBRecords, $pageindex_rmbwithdrawrecords, function (records, pageindex) {

            $pageindex_rmbwithdrawrecords = pageindex;
            var html = '';
            $(records).each(function () {
                var $status = '';
                if(this.State == 0){
                    $status = '处理中';
                } else if (this.State == 1) {
                    $status = '成功';
                } else if (this.State == 2) {
                    $status = '被拒绝';
                }
                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_rmbwithdrawrecords,
                    time: this.CreateTimeString,
                    count: '提现灵币: ' + this.WidthdrawRMB + $status,
                });
                html += rowhtml;
            });

            return html;
        });
    };

    function getUserBuyStoneRecordList_Simple() {

        var $listviewUserBuyStoneRecords = $("div[data-id='navstonebuyrecords']");

        getRecordList("AsyncGetBuyStoneRecordHandler", $listviewUserBuyStoneRecords, $pageindex_stonebuyrecords, function (records, pageindex) {

            $pageindex_stonebuyrecords = pageindex;
            var html = '';
            $(records).each(function () {

                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_stonebuyrecords,
                    time: this.BuyTimeString,
                    count: '购买矿石: ' + this.StonesOrder.SellStonesCount,
                });
                html += rowhtml;
            });

            return html;
        });
    };

    function getUserSellStoneRecordList_Simple() {

        var $listviewUserSellStoneRecords = $("div[data-id='navstonesellrecords']");

        getRecordList("AsyncGetUserSellStoneRecordHandler", $listviewUserSellStoneRecords, $pageindex_stonesellrecords, function (records, pageindex) {

            $pageindex_stonesellrecords = pageindex;
            var html = '';
            $(records).each(function () {
                var $OrderState = '';
                if (this.OrderStateInt == 1) {
                    $OrderState = '等待交易';
                } else if (this.OrderStateInt == 2) {
                    $OrderState = '被锁定';
                } else if (this.OrderStateInt == 3) {
                    $OrderState = '成功';
                } else if (this.OrderStateInt == 4) {
                    $OrderState = '异常';
                }
                var rowhtml = $itemHtmlRecord.format({
                    pageindex: $pageindex_stonesellrecords,
                    time: this.SellTimeString,
                    count: '出售矿石: ' + this.SellStonesCount + $OrderState,
                });
                html += rowhtml;
            });

            return html;
        });
    };

    function getRecordList(baseurl, $listview, $pageIndex, gethtmlhandler) {

        //var $listviewMineRecords = $("div[data-id='navminerecords']");

        $divloading.css("display", "block");
        $btnloadmore.css("display", "none");

        var $lastItem = $listview.children("a.weui-cell").last();
        if ($lastItem != 'undefined' && $lastItem.length != 0) {
            $pageIndex = $lastItem.data("pageindex");
        } else {
            $pageIndex = 0;
        }

        var $url = baseurl + "?pageitemcount=" + $pageitemcount + "&pageindex=" + ($pageIndex + 1);

        $.get($url, function (data, status) {

            $btnloadmore.css("display", "block");
            $divloading.css("display", "none");

            if (data.length == 0) {
                showMessageDialog("获取数据失败");
            }
            else {
                if (data.length > 1) {
                    var operStatus = data.substring(0, 1);
                    if (operStatus == "0") {
                        var message = data.substring(1, data.length);
                        showMessageDialog(message);
                    }
                    else {
                        var orders = eval("(" + data.substring(1, data.length) + ")");
                        if (orders == 'undefine') {
                            showMessageDialog("加载数据失败");
                        } else {
                            //只在获取满页，才加页数

                            $pageIndex += 1;
                            var html = gethtmlhandler(orders, $pageIndex);

                            $listview.append(html);

                        }
                    }
                }
            }
        });
    }


    var $itemHtmlRecord =
            "<a class=\"weui-cell weui-cell_access\" data-pageindex=\"{pageindex}\" href=\"javascript:;\">" +
            "    <div class=\"weui-cell__bd\">" +
            "        <p>{time}</p>" +
            "    </div>" +
            "    <div class=\"weui-cell__ft\">{count}</div>" +
            "</a>";


    var i = 0;
    $(".weui-navbar__item").each(function () {
        var left = (100 * i) + "px"
        i++;
        $(this).css("left", left);
    });

    var Scroll = new iScroll('wrapper', { hScrollbar: false, vScrollbar: false });
});