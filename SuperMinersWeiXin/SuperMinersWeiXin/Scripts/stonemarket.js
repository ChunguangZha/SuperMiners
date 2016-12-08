
$(function () {
    $(".btn_buystone").on('click', function () {
        var $this = $(this);
        var orderid = $this.data("orderid");
        var rmb = $this.data("rmb");
        showConfirmDialog("请确认", "您需要支付" + rmb + "灵币", "购买", "取消", function () {
            $.get("StoneMarket.aspx?a=" + orderid);
        });
    });
});