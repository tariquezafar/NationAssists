function SaveServicePrice() {
    if (ValidateForm()) {
        var objPrice = {
            ServicePriceId: $("#hdnServicePriceId").val(),
            ServiceId: $("#ServiceId").val(),
            Prices: $("#txtPrice").val(),
            StartDate: $("#txtStartDate").val(),
            EndDate: $("#txtEndDate").val()
            
        };
        var pUrl = "/Admin/Service/SavePrices/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objPrice),
            success: function (data) {
                if (data.Result) {
                    alert("Price is successfully " + ($("#hdnServicePriceId").val() == "0" ? "added." : "updated."));
                    window.location.href = "/admin/Service/ManageServicePrice/";
                }
                else {
                    alert("Opps! some error occured.");
                }
            },
            error: function (data) {
            }
        });
    }
}

function ValidateForm() {
    var IsValid = true;
    var strErrMsg = "";
    if ($('#ServiceId').val() == "") {
        IsValid = false;
        strErrMsg += "Please select Service. \n";
    }
    if ($("#txtPrice").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Price. \n";
    }
    if ($("#txtStartDate").val() == "") {
        IsValid = false;
        strErrMsg += "Please select start date. \n";
    }
    if ($("#txtEndDate").val() == "") {
        IsValid = false;
        strErrMsg += "Please select end date.";
    }
    if ($("#txtEndDate").val() != "" && $("#txtStartDate").val() != "" && Date.parse($("#txtAggreementEndDate").val()) <= Date.parse($("#txtStartDate").val())) {

        IsValid = false;
        strErrMsg += "End Date should be greater than start date.. \n";
    }

    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function BindServicePriceList() {
    if ( $("#ServiceId").val() != "") {

        var pUrl = "/Admin/Service/ShowServicePriceList?ServiceId=" + $("#ServiceId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                if (!IsJsonString(data)) {
                    $("#tblPriceList").html(data);
                }

            }
        });
    }
}

function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}