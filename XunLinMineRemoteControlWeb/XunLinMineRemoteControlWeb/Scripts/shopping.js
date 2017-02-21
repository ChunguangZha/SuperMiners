$().ready(function () {
    $("#ContentPlaceHolder1_serverTypeSelect").change(servertypeselectedchanged);
});

function servertypeselectedchanged(){
    var selectedValue = $("#ContentPlaceHolder1_serverTypeSelect").val();
    var price = 0;
    if (selectedValue == "once") {
        price = 50;
    }
    else if (selectedValue == "onemonth") {
        price = 300;

    } else if (selectedValue == "halfyear") {
        price = 2000;

    } else if (selectedValue == "oneyear") {

        price = 5000;

    } else {
        price = 10000;
    }

    $("#ContentPlaceHolder1_lblPrice").text(price);
}