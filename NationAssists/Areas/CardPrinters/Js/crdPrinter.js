function getDataPageWise(e)
{
    var capabilities = "";
    var brands = "";
    var recordPerPage = 3;
    var pUrl = "/CardPrinters/CardPrinter/GetDataPageWise?Page=" + $(e).html() + "&RecordPerPage=" + recordPerPage + "  &Capabilities=" + capabilities + "&Brands=" + brands;
    $.ajax({
        type: "POST",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {
            debugger;
            $("#PrintList").html(data);

        },
        error: function (data) {
        }
    });

}

var arrCapabality = [];
var arrBrands = [];

function filterData(e)
{
    
    $("input[type='checkbox']:checked").each(function (i, e) {

        if($(e).attr("filterType")=="brand")
        {
            arrBrands.push($(e).attr("filterid"));
        }
        else {
            arrCapabality.push($(e).attr("filterid"));
        }
    });

    //if ($(e).prop("checked"))
    //{
    //    if ($(e).attr("filterType") == "brand")
    //    {
    //        arrBrands.push($(e).attr("filterid"));
    //    }
    //    else {
    //        arrCapabality.push($(e).attr("filterid"));
    //    }
    //}
    //else {
    //    if ($(e).attr("filterType") == "brand") {
    //        arrBrands.splice($(e).attr("filterid"), 1);
    //    }
    //    else {
    //        arrCapabality.splice($(e).attr("filterid"), 1);
    //    }
    //}

    
    


    var pUrl = "/CardPrinters/CardPrinter/GetDataPageWise?Page=1 &RecordPerPage=3&Capabilities=" + arrCapabality.toString() + "&Brands=" + arrBrands.toString();
    $.ajax({
        type: "POST",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {
            debugger;
            $("#PrintList").html(data);

        },
        error: function (data) {
        }
    });

}



