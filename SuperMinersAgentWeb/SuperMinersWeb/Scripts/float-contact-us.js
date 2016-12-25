
$().ready(function () {

    var flag = 1;
    $('#rightArrow').on("click", function () {
        if (flag == 1) {
            $("#floatDivBoxs").animate({ right: '-175px' }, 300);
            $(this).animate({ right: '-5px' }, 300);
            $(this).css('background-position', '-50px 0');
            flag = 0;
        } else {
            $("#floatDivBoxs").animate({ right: '0' }, 300);
            $(this).animate({ right: '170px' }, 300);
            $(this).css('background-position', '0px 0');
            flag = 1;
        }
    });

    var flag2 = 1;
    $('#rightArrowWeiXinImg').on("click", function () {
        if (flag2 == 1) {
            $("#floatLeftDiv").animate({ left: '-256px' }, 300);
            $(this).animate({ left: '-5px' }, 300);
            $(this).css('background-position', '0px 0');
            flag2 = 0;
        } else {
            $("#floatLeftDiv").animate({ left: '0' }, 300);
            $(this).animate({ left: '256px' }, 300);
            $(this).css('background-position', '-50px 0');
            flag2 = 1;
        }
    });
});