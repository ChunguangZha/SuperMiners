
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
