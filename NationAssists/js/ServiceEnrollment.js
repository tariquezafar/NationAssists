function BindGovernotes() {
    if ($("#CountryId").val() != "" && $("#CountryId").val() != "0") {
        var pUrl = "/RaiseServiceRequest/BindGovernotes?CountryId=" + $("#CountryId").val();
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

                    if ($("#hdnGovernotesId").val() == "0") {
                        $("#PlaceId").html($('<option></option>').val(0).html("--Select--"));
                        $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));
                    }
                    else {
                        $("#GovernotesId").val($("#hdnGovernotesId").val());
                        BindPlaces();
                    }

                }
            }
        });
    }
}

function BindPlaces() {
    if ($("#GovernotesId").val() != "" && $("#GovernotesId").val() != "0") {
        var pUrl = "/RaiseServiceRequest/BindPlaces?GovernotesId=" + $("#GovernotesId").val();
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

                    if ($("#hdnPlaceId").val() == "0") {
                        $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));
                    }
                    else {
                        $("#PlaceId").val($("#hdnPlaceId").val());
                        BindBlock();
                    }

                }
            }
        });
    }
}

function BindBlock() {
    if ($("#PlaceId").val() != "" && $("#PlaceId").val() != "0") {
        var pUrl = "/RaiseServiceRequest/BindBlocks?PlaceId=" + $("#PlaceId").val();
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
                    $("#BlockCode").val($("#hdnBlockId").val());




                }
            }
        });
    }
}


function ValidateForm() {

    var IsValid = true;
    var strErrMsg = "";
    
    
    if ($("#PackageId").val() != "0") {

        var ServiceCode = $('#hdnPackageCode').val();
        if (ServiceCode == "HAP") {

            if ($("#txtRiskAddress").val() == "") {

                IsValid = false;
                strErrMsg += "Please enter Risk Address . \n";
            }
            if ($('#CountryId').val() == "" || $('#CountryId').val() == "0") {
                IsValid = false;
                strErrMsg += "Please select Country. \n";
            }


            if ($('#CountryId').val() != "" && $('#GovernotesId').val() == "0") {
                IsValid = false;
                strErrMsg += "Please select Governotes. \n";
            }

            if ($('#GovernotesId').val() != "0" && $('#PlaceId').val() == "0") {
                IsValid = false;
                strErrMsg += "Please select Place. \n";
            }
            if ($('#PlaceId').val() != "0" && $('#BlockCode').val() == "0") {
                IsValid = false;
                strErrMsg += "Please select Block. \n";
            }
            if ($("#txtNoOfLocation").val() == "" || $("#txtNoOfLocation").val() == "0") {
                IsValid = false;
                strErrMsg += "Please enter No of Location . \n";
            }
            if ($("#txtTypeOfService").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Type of Services. \n";
            }
        }
        else {

            if ($("#txtVehicleMake").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle Make. \n";
            }

            if ($("#txtVehicleType").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle Type. \n";
            }
            if ($("#txtVehicleBody").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle Body. \n";
            }
            
            if ($("#txtVehicleRegistrationNo").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter vehicle registration No. \n";
            }
            if ($("#txtChassisNo").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Chassis No. \n";
            }
            if ($("#txtVehicleYear").val() == "" || $("#txtVehicleYear").val() == "0") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle year . \n";
            }
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


function SaveMembership() {

    if (ValidateForm()) {

        var objMembership = {
            CustomerId: $("#hdnCustomerId").val(),
            ServiceId: $("#PackageId").val(),
            VehicleRegistrationNumber: $("#txtVehicleRegistrationNo").val(),
            VehicleBody: $("#txtVehicleBody").val(),
            ModelType: $("#txtVehicleType").val(),
            YearOfManufacture: $("#txtVehicleYear").val(),
            YearOfConstruction: $("#txtYearOfConstruction").val(),
            ChessisNo: $("#txtChassisNo").val(),
            CountryId: $("#hdnPackageCode").val()=="HAP"? $("#CountryId").val():null,
            GovernotesId: $("#hdnPackageCode").val() == "HAP" ? $("#GovernotesId").val():null,
            PlaceId: $("#hdnPackageCode").val() == "HAP" ? $("#PlaceId").val():null,
            BlockId: $("#hdnPackageCode").val() == "HAP" ? $("#BlockCode").val():null,
            NoOfLocation: $("#txtNoOfLocation").val(),
            RiskAddress: $("#txtRiskAddress").val(),
            VehicleManufacturer: $("#txtVehicleMake").val(),
            TypeOfService: $("#txtTypeOfService").val(),
            Remarks: $("#txtRemarks").val(),
            ServiceEnrollmentRequestId: $("#hdnServiceEnrollmentId").val()

        };



        var pUrl = "/BuyPackageRequest/SaveServiceEnrollmentRequest/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objMembership),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("Thanks for raising service Enrollment Request. ");
                    window.location.href = "/BuyPackageRequest";
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

function ShowServiceEnrollmentDetail(e) {
    $("#hdnServiceEnrollmentId").val(e.ServiceEnrollmentRequestId);
    $("#PackageId").html("<option value='" + e.ServiceId + "' > " + e.ServiceName + "</option>");
    $("#hdnPackageId").val(e.ServiceId); 
    $("#hdnCountryId").val(e.CountryId);
    $("#CountryId").val(e.CountryId);
    $("#hdnGovernotesId").val(e.GovernotesId);
    $("#hdnPlaceId").val(e.PlaceId);
    $("#hdnBlockId").val(e.BlockId);
    BindGovernotes();
    var ServiceCode = e.ServiceCode;
    $("#txtChassisNo").val(e.ChessisNo);
    $("#txtVehicleType").val(e.ModelType);
    $("#txtNoOfLocation").val(e.NoOfLocation);
    $("#txtRemarks").val(e.Remarks);
    $("#txtRiskAddress").val(e.RiskAddress);
    $("#txtTypeOfService").val(e.TypeOfService);
    $("#txtVehicleBody").val(e.VehicleBody);
    $("#txtVehicleMake").val(e.VehicleManufacturer);
    $("#txtVehicleRegistrationNo").val(e.VehicleRegistrationNumber);
    $("#txtYearOfConstruction").val(e.YearOfConstruction);
    $("#txtVehicleYear").val(e.YearOfManufacture);
    $("#txtApproverRemarks").val(e.ApproverRemarks);
    if (ServiceCode == "HAP") {
        $(".HAS").show();
        $(".RAS").hide();

    }
    else {
        $(".HAS").hide();
        $(".RAS").show();
    }
    $("#dvServiceDetail").show();

    if (e.ServiceEnrollmentStatus !=4) {
        $('input').attr("disabled", "disabled");
        $('select').attr("disabled", "disabled");
        $("#dvButton").hide();
    }
    else {
        $('input').attr("disabled", false);
        $('select').attr("disabled", false);
        $("#dvButton").show();
        $("#liApproverRemarks").show();
    }

    
  
    
}