$(function () {

    var $pageitemcount = 10;
    var $pageindex = 0;
    var $loadmore = $("#loadmore");
    $loadmore.on('click', function () {
        getMinerBuyRecordList();
    });

    function getMinerBuyRecordList() {

        var $listview = $("#listitems");
        var $loading = $("#loading");

        $loading.css("display", "block");
        $loadmore.css("display", "none");

        var $lastItem = $("div#listitems a.weui-cell").last();
        if ($lastItem != 'undefined' && $lastItem.length != 0) {
            $pageindex = $lastItem.data("pageindex");
        }

        var $url = "AsyncGetMinerBuyRecordHandler?pageitemcount=" + $pageitemcount + "&pageindex=" + ($pageindex + 1);

        $.get($url, function (data, status) {

            $loadmore.css("display", "block");
            $loading.css("display", "none");

            if (data.length == 0) {
                showMessageDialog("获取数据失败");
            }
            else {
                if (data.length > 1) {
                    $pageindex += 1;
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
                            var html = '';
                            $(orders).each(function () {
                                var rowhtml = $itemHtmlMineBuyRecord.format({
                                    pageindex: $pageindex,
                                    time: this.MyTime.Year + '-' + this.MyTime.Month + '-' + this.MyTime.Day + ' ' + this.MyTime.Hour + '-' + this.MyTime.Minute + '-' + this.MyTime.Second,
                                    minercount: this.GainMinersCount,
                                });
                                html += rowhtml;
                            });

                            $listview.append(html);

                            //$(".btn_buystone").on('click', function () {
                            //    var $this = $(this);
                            //    var orderid = $this.data("orderid");
                            //    var rmb = $this.data("rmb");
                            //    var seller = $this.data("seller");
                            //    showConfirmDialog("请确认", "您需要支付" + rmb + "灵币", "购买", "取消", function () {
                            //        $.post("BuyStoneHandler", { orderNumber: orderid, sellerUserName: seller }, function (data, status) {

                            //            if (data == "OK") {
                            //                getSellStoneList();
                            //                showOKToast();
                            //            }
                            //            else {
                            //                showMessageDialog(data);
                            //            }
                            //        });
                            //    });
                            //});

                        }
                    }
                }
            }
        });
    }

    var $itemHtmlMineBuyRecord =
            "<a class=\"weui-cell weui-cell_access\" data-pageindex=\"{pageindex}\" href=\"javascript:;\">" +
            "    <div class=\"weui-cell__bd\">" +
            "        <p>{time}</p>" +
            "    </div>" +
            "    <div class=\"weui-cell__ft\">购买矿工{minercount}</div>" +
            "</a>";

    getMinerBuyRecordList();
});