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

function BindServiceType() {
    if ($("#ServiceId").val() != "" && $("#ServiceId").val() != "0") {
        var pUrl = "../../RaiseServiceRequest/BindServiceType?ServiceId=" + $("#ServiceId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#ServiceSubCategoryId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#ServiceSubCategoryId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceSubCategoryId").append(
                            $('<option></option>').val(service.ServiceSubCategoryId).html(service.Name));
                    });

                }
            }
        });
    }
}


function BindGovernotes() {
    if ($("#CountryId").val() != "" && $("#CountryId").val() != "0") {
        var pUrl = "../../RaiseServiceRequest/BindGovernotes?CountryId=" + $("#CountryId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#GovernotesId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#GovernotesId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#GovernotesId").append(
                            $('<option></option>').val(service.GovernorateId).html(service.GovernoratesName));
                    });

                    $("#PlaceId").html($('<option></option>').val(0).html("--Select--"));
                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

                }
            }
        });
    }
}

function BindPlaces() {
    if ($("#GovernotesId").val() != "" && $("#GovernotesId").val() != "0") {
        var pUrl = "../../RaiseServiceRequest/BindPlaces?GovernotesId=" + $("#GovernotesId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#PlaceId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#PlaceId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#PlaceId").append(
                            $('<option></option>').val(service.PlaceId).html(service.Place_Name));
                    });

                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

                }
            }
        });
    }
}

function BindBlock() {
    if ($("#PlaceId").val() != "" && $("#PlaceId").val() != "0") {
        var pUrl = "../../RaiseServiceRequest/BindBlocks?PlaceId=" + $("#PlaceId").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#BlockCode").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#BlockCode").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#BlockCode").append(
                            $('<option></option>').val(service.BlockId).html(service.Block_Code));
                    });



                }
            }
        });
    }
}