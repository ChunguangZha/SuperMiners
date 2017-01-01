
$(function () {
    $("#abtnRefreshSellStoneList").on('click', getSellStoneList);

    function getSellStoneList () {
        showLoadingToast();
        $.get("AsyncGetSellStoneOrderHandler", function (data, status) {
            closeLoadingToast();
            if (data.length == 0) {
                showMessageDialog("获取数据失败");
            }
            else {
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

                            var imgCreditLevelHtml = "";
                            if (this.SellerCreditLevel > 0) {
                                imgCreditLevelHtml = "<img align='middle' src='../images/l" + this.SellerCreditLevel + ".png' />";
                            }

                            var imgExpLevelHtml = "";
                            if (this.SellerExpLevel > 0) {
                                imgExpLevelHtml = "<img align='middle' src='../images/vip" + this.SellerExpLevel + ".png' />";
                            }

                            var rowhtml = $itemHtmlSellStoneOrder.format({
                                ValueRMB: this.ValueRMB,
                                OrderNumber: this.OrderNumber,
                                SellStonesCount: this.SellStonesCount,
                                SellerUserName: this.SellerUserName,
                                CreditLevelImg: imgCreditLevelHtml,
                                ExpLevelImg: imgExpLevelHtml,
                            });
                            html += rowhtml;
                        });

                        $(".listview").html(html);
                        $(".btn_buystone").on('click', function () {
                            var $this = $(this);
                            var orderid = $this.data("orderid");
                            var rmb = $this.data("rmb");
                            var seller = $this.data("seller");
                            showConfirmDialog("请确认", "您需要支付" + rmb + "灵币", "购买", "取消", function () {
                                $.post("BuyStoneHandler", { orderNumber: orderid, sellerUserName: seller }, function (data, status) {

                                    if (data == "OK") {
                                        getSellStoneList();
                                        showOKToast();
                                    }
                                    else {
                                        showMessageDialog(data);
                                    }
                                });
                            });
                        });

                    }
                }
            }
        });
    }

    getSellStoneList();

    var $itemHtmlSellStoneOrder =     
                "<div class='weui-form-preview'>" +
                "    <div class='weui-form-preview__hd'>"+
                "        <div class='weui-form-preview__item'>"+
                "            <label class='weui-form-preview__label'>付款灵币</label>"+
                "            <em class='weui-form-preview__value'>{ValueRMB}</em>"+
                "        </div>"+
                "    </div>"+
                "    <div class='weui-form-preview__bd'>"+
                "        <div class='weui-form-preview__item'>"+
                "            <label class='weui-form-preview__label'>订单号</label>"+
                "            <span class='weui-form-preview__value'>{OrderNumber}</span>"+
                "        </div>"+
                "        <div class='weui-form-preview__item'>"+
                "            <label class='weui-form-preview__label'>矿石数</label>"+
                "            <span class='weui-form-preview__value'>{SellStonesCount}</span>"+
                "        </div>"+
                "        <div class='weui-form-preview__item'>"+
                "            <label class='weui-form-preview__label'>卖家</label>"+
                "            <span class='weui-form-preview__value'>{SellerUserName}{ExpLevelImg}{CreditLevelImg}</span>" +
                "        </div>"+
                "    </div>"+
                "    <div class='weui-form-preview__ft'>"+
                "        <a class='btn_buystone weui-form-preview__btn weui-form-preview__btn_primary' style='display:none;' data-orderid='{OrderNumber}' data-rmb='{ValueRMB}' data-seller='{SellerUserName}'>购买</a>" +
                "    </div>"+
                "</div>";
});