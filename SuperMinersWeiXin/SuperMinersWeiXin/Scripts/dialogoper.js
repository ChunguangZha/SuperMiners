
var $handler;
var $inputValueVerfy;

$(function () {
    var $iosDialog1 = $('#iosDialog1'),
        $iosDialog2 = $('#iosDialog2');
    var $ctrInput = $("#dig3_inputValue");

    $ctrInput.focus(function () {
        $(this).select();
    });

    $('#dialogs').on('click', '.weui-dialog__btn', function () {

        var $this = $(this);
        if ($this.is('.weui-dialog__btn_default')) {
            $(this).parents('.js_dialog').fadeOut(200);
            $ctrInput.val("0");
            return;
        }

        var $inputvalue;
        var $data_id = $this.data("id");
        if ($data_id != 'undefined' && $data_id == "dig3") {
            var szReg = /^([1-9][0-9]*)$/;
            $inputvalue = $ctrInput.val();
            if ($inputvalue == 'undefined' || $inputvalue == '' || !szReg.test($inputvalue)) {

                $ctrInput.addClass('weui-input_error');
                setTimeout(function () {
                    $ctrInput.removeClass('weui-input_error');
                }, 1000);
                return;
            }
            if ($inputValueVerfy != 'undefined' && typeof $inputValueVerfy == 'function') {
                if (!$inputValueVerfy($inputvalue)) {

                    $ctrInput.addClass('weui-input_error');
                    setTimeout(function () {
                        $ctrInput.removeClass('weui-input_error');
                    }, 1000);
                    return;
                }
            }
        }

        $(this).parents('.js_dialog').fadeOut(200);
        if ($handler != 'undefined' && typeof $handler == 'function') {
            if ($inputvalue == 'undefined' || $inputvalue == '') {
                $handler();
            } else {
                $handler($inputvalue);
            }
            $ctrInput.val("0");
        }
    });

});

function showConfirmDialog(title, message, primaryAction, secondAction, handler) {
    var $title = $("#dig1_title");
    $title.text(title);
    var $msg = $("#dig1_msg");
    $msg.text(message);
    var $primaryAction = $("#dig1_primaryAction");
    $primaryAction.text(primaryAction);
    var $secondaryAction = $("#dig1_secondAction");
    $secondaryAction.text(secondAction);

    var $iosDialog = $('#iosDialog1')
    $iosDialog.fadeIn(200);

    $handler = handler;
}

function showMessageDialog(message, handler) {
    var $msg = $("#dig2_msg");
    $msg.text(message);

    var $iosDialog = $('#iosDialog2')
    $iosDialog.fadeIn(200);

    $handler = handler;
}

function showInputNumberDialog(title, placeholder, handler, inputValueVerfy) {
    var $title = $("#dig3_title");
    $title.text(title);
    $("#dig3_tips").text(placeholder);

    var $iosDialog = $('#iosDialog3')
    $iosDialog.fadeIn(200);

    $handler = handler;
    $inputValueVerfy = inputValueVerfy;
}
