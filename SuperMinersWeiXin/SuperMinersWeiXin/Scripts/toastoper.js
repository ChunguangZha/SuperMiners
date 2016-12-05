
function showLoadingToast() {
    var $loadingToast = $('#loadingToast');
    if ($loadingToast.css('display') != 'none') return;

    $loadingToast.fadeIn(100);
    //setTimeout(function () {
    //    $loadingToast.fadeOut(100);
    //}, 2000);
}

function closeLoadingToast() {
    var $loadingToast = $('#loadingToast');
    if ($loadingToast.css('display') == 'none') return;
    $loadingToast.fadeOut(100);
}

function showOKToast() {
    var $toast = $('#toast');
    if ($toast.css('display') != 'none') return;

    $toast.fadeIn(100);
    setTimeout(function () {
        $toast.fadeOut(100);
    }, 2000);
}
