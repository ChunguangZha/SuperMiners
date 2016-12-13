$(function () {

    var $pageitemcount=10;
    var $pageindex = 0;

    
    function getMineBuyRecordList() {

        var $listview = $(".listview");
        var $loading = $("#loading");
        var $loadmore = $("#loadmore");

        $loading.css("display", "block");
        $loadmore.css("display", "none");

        var $lastItem = $("div#listview a.weui-cell").last();
        if ($lastItem != 'undefined' && $lastItem.length != 0) {
            $pageindex = $lastItem.data("pageindex");
        }

        var $url = "AsyncGetMineBuyRecordHandler?pageitemcount=" + $pageitemcount + "&pageindex=" + ($pageindex + 1);
        
        $.get($url, function (data, status) {
            
            $loadmore.css("display", "block");
            $loading.css("display", "none");

            if (data.length == 0) {
                showMessageDialog("获取数据失败");
            }
            else {
                if (data.length != 1) {

                    var operStatus = data.substring(0, 1);
                    if (operStatus == "0") {
                        var message = data.substring(1, data.length);
                        showMessageDialog(message);
                    }
                    else {
                        $pageindex += 1;

                        var orders = eval("(" + data.substring(1, data.length) + ")");
                        if (orders == 'undefine') {
                            showMessageDialog("加载数据失败");
                        } else {
                            var html = '';
                            $(orders).each(function () {
                                var rowhtml = $itemHtmlMineBuyRecord.format({
                                    pageindex: $pageindex,
                                    time: this.MyPayTime,
                                    reservers: this.GainStonesReserves,
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
            "    <div class=\"weui-cell__ft\">收获储量{reservers}</div>" +
            "</a>";

    getMineBuyRecordList();
});