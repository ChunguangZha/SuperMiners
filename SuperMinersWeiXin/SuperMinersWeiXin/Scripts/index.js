$(document).ready(function () {
    $(function () {
        $('.weui-tabbar__item').on('click', function () {
            $(this).addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
        });
    });

    var $pagediv = $(this).find('.root_subpage');
    var $pageid = $pagediv.data("id");

    var $currenta = $(this).find('a[data-id=' + $pageid + ']');
    if ($currenta != null) {
        $currenta.addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
    }
});