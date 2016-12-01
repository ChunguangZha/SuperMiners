$(document).ready(function () {
    $(function () {
        $('.weui-tabbar__item').on('click', function () {
            $(this).addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
            hideActionSheet();
        });
    });

    var $pagediv = $(this).find('.root_subpage');
    var $pageid = $pagediv.data("id");

    var $currenta = $(this).find('a[data-id=' + $pageid + ']');
    if ($currenta != null) {
        $currenta.addClass('weui-bar__item_on').siblings('.weui-bar__item_on').removeClass('weui-bar__item_on');
    }

    $('#menu_records').on('click', showrecords_submenus);
    var $iosMask = $('#iosMask');
    $iosMask.on('click', hideActionSheet);
});

// ios
function showrecords_submenus() {
    var $iosActionsheet = $('#iosActionsheet');
    var $iosMask = $('#iosMask');
    $iosActionsheet.addClass('weui-actionsheet_toggle');
    $iosMask.fadeIn(200);

}

function hideActionSheet() {
    var $iosActionsheet = $('#iosActionsheet');
    var $iosMask = $('#iosMask');
    $iosActionsheet.removeClass('weui-actionsheet_toggle');
    $iosMask.fadeOut(200);
}
