﻿function BindServiceSubCategoryPriceList() {
    if ($("#ServiceProviderId").val() != "-1" && $("#ServiceId").val() != "0") {

        var pUrl = "/Admin/Service/ShowServiceSubCategoryPriceList?ServiceProviderId=" + $("#ServiceProviderId").val() + "&ServiceId=" + $("#ServiceId").val();
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
                    $("input[type=radio][name=PricingOption][value=SB]").prop("checked", true);
                    $('.pb').hide()
                    $("#tblPriceList").html(data);
                }
                else {
                   
                    var JData = JSON.parse(data);
                    if (JData.Error != "") {

                        var objServiceCategoryPrice = JData.ServiceProviderPrice;
                        $("input[type=radio][name=PricingOption][value=" + objServiceCategoryPrice.PriceOption + "]").prop("checked", true);
                        $('.pb').show()

                    }
                    
                }
                
            }
        });
    }
}

function GetAllServices() {

    if ($("#ServiceProviderId").val() != "") {
        var ServiceProviderId = $("#ServiceProviderId").val();
        var pUrl = "/Admin/Service/BindServices?ServiceProviderId=" + ServiceProviderId;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                $('.pb').hide();
                $('#tblPriceList').html('');
                var jData = JSON.parse(data);
                $("#ServiceId").html(""); // clear before appending new list
                if (jData != null && jData.length > 0) {
                    $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceId").append(
                            $('<option></option>').val(service.ServiceId).html(service.ServiceName));
                    });
                }
            }
        });
    }

}


function SaveServiceProviderPrice() {

    if (ValidateForm()) {

        var SubCategoryPriceList = [];
        $("#tblServicePriceList tr").each(function (i, e) {
            var ServiceSubCategoryPrice = {
                ServiceSubCategoryId: $(e).find('input[id="SubCategoryId"]').val(),
                Price: $(e).find('input[name="txtPrice"]').val(),
                StartDate: $(e).find('input[name="txtStartDate"]').val(),
                EndDate: $(e).find('input[name="txtEndDate"]').val()
            };

            SubCategoryPriceList.push(ServiceSubCategoryPrice);

        });

        var objServiceSubCategoryPrice = {
            ServiceProviderId: $("#ServiceProviderId").val(),
            ServiceId: $("#ServiceId").val(),
            SubCategoryPriceList: SubCategoryPriceList
        };



        var pUrl = "/Admin/Service/SaveServiceProviderPrice/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objServiceSubCategoryPrice),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Price Save successfully "); 
                    window.location.href = "/admin/Service/ManageServiceProviderPrice/";
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
    if ($('#ServiceProviderId').val() == "") {
        IsValid = false;
        strErrMsg += "Please select Service Provider. \n";
    }
    if ($('#ServiceId').val() == "" || $('#ServiceId').val() == "0" ) {
        IsValid = false;
        strErrMsg += "Please select Service. \n";
    }

    if ($("input[type=radio][name=PricingOption]:checked").val()=="SB" && $("#tblServicePriceList tr").length > 0)
    {
        $("#tblServicePriceList tr").each(function (i, e) {
            if ($(e).find('input[name="txtPrice"]').val() == "" ) {
                IsValid = false;
                strErrMsg += "Please enter Price for Row No "+ (i+1)+". \n";
            }
            if ($(e).find('input[name="txtStartDate"]').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Start Date for Row No " + (i + 1) + ". \n";
            }

            if ($(e).find('input[name="txtEndDate"]').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter End Date for Row No " + (i + 1) + ". \n";
            }

            if ($(e).find('input[name="txtEndDate"]').val() != "" && $(e).find('input[name="txtStartDate"]').val() != "" && Date.parse($(e).find('input[name="txtEndDate"]').val()) <= Date.parse($(e).find('input[name="txtStartDate"]').val())) {

                IsValid = false;
                strErrMsg += "End Date should be greater than start date for Row No " + (i + 1)+" \n";
            }
        });
    }

    if ($("input[type=radio][name=PricingOption]:checked").val() == "PB") {
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

    }

    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
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

