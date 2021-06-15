﻿
$loading = null;
//Attach the event handler to any element
$(document).ready(function () {
    $loading = $('#progress');

    $(document).ajaxStart(function () {
        
        var IsLoaderRequired = $('body').data("IsLoaderRequired");
        if (IsLoaderRequired) {
            console.log("started");

            //ajax request went so show the loading image
            $loading.show();
        }
     
    });
    $(document).ajaxStop(function () {
        var IsLoaderRequired = $('body').data("IsLoaderRequired");
        if (IsLoaderRequired) {
            console.log("stop");

            //ajax request went so show the loading image
            $loading.hide();
        }
       
    });
});

function RestrictAlphabetAndSpace(id) {
    $(function () {
        $("#" + id).keydown(function (e) {
            
            if (e.shiftKey || e.ctrlKey || e.altKey) {
                e.preventDefault();
            }
            else {
                var key = e.keyCode;
                if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                    e.preventDefault();
                }
            }
        });
    });
}


function formatDateWithTime(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();
    hour = d.getHours();
    minute = d.getMinutes();


    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    var datefiled = [year, month, day].join('-');
    return (datefiled + "T" + hour.toString() + ":" + minute.toString());
}



function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();



    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');

}



function formatDateWithAMPM(dateVal) {
    var newDate = new Date(dateVal);

    var sMonth = padValue(newDate.getMonth() + 1);
    var sDay = padValue(newDate.getDate());
    var sYear = newDate.getFullYear();
    var sHour = newDate.getHours();
    var sMinute = padValue(newDate.getMinutes());
    var sAMPM = "AM";

    var iHourCheck = parseInt(sHour);

    if (iHourCheck > 12) {
        sAMPM = "PM";
        sHour = iHourCheck - 12;
    }
    else if (iHourCheck === 0) {
        sHour = "12";
    }

    sHour = padValue(sHour);

    return sMonth + "-" + sDay + "-" + sYear + " " + sHour + ":" + sMinute + " " + sAMPM;
}
function padValue(value) {
    return (value < 10) ? "0" + value : value;
}



function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}


