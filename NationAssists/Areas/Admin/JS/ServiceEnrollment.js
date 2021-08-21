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
    
    
    if ($("#ServiceEnrollmentStatusId").val() == "" || $("#ServiceEnrollmentStatusId").val() == "0") {

        IsValid = false;
        strErrMsg += "Please select Enrollment Status . \n";
    }
    else {
        if ($("#ServiceEnrollmentStatusId").val() == "2") {
            //if ($("#txtPolicyType").val() == "") {
            //    IsValid = false;
            //    strErrMsg += "Please enter Policy Type . \n";
            //}
            //if ($("#txtPolicyNo").val() == "") {
            //    IsValid = false;
            //    strErrMsg += "Please enter Policy No . \n";
            //}

            if ($("#txtEffectiveDate").val() == "") {
                IsValid = false;
                strErrMsg += "Please select Policy Effective Date . \n";
            }

            if ($("#txtEffectiveDate").val() != "" && Date.parse($("#txtEffectiveDate").val()) < Date.parse($("#hdnRequestedDate").val()) ) {
                IsValid = false;
                strErrMsg += "Policy Effective date must 3 days after than Service requested Date . \n";
            }
            if ($("#txtExpiryDate").val() != "" && $("#txtEffectiveDate").val() != "" && Date.parse($("#txtExpiryDate").val()) <= Date.parse($("#txtEffectiveDate").val())) {

                IsValid = false;
                strErrMsg += "Policy expiry  Date should be greater than policy effective date.. \n";
            }


        }
        else {
            if ($("#txtApproverRemarks").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Approver Remarks . \n";

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
            ServiceEnrollmentRequestId: $("#hdnServiceEnrollmentId").val(),
            Branch: $("#txtBranch").val(),
            PolicyType: $("#txtPolicyType").val(),
            InsurancePolicyNumber: $("#txtPolicyNo").val(),
            PolicyStartDate: $("#txtEffectiveDate").val(),
            PolicyEndDate: $("#txtExpiryDate").val(),
            ServiceEnrollmentStatus: $("#ServiceEnrollmentStatusId").val(),
            ApproverRemarks: $("#txtApproverRemarks").val(),


        };



        var pUrl = "/Admin/ServiceEnrollment/UpdateServiceEnrollment";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objMembership),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert("service Enrollment Request has been updated successfully. ");
                    window.location.href = "/Admin/ServiceEnrollment";
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
    $("#dvDetail").hide();
    $("#hdnServiceEnrollmentId").val(e.ServiceEnrollmentRequestId);
    $("#PackageId").html("<option value='" + e.ServiceId + "' > " + e.ServiceName + "</option>");
    $("#hdnPackageId").val(e.ServiceId); 
    $("#hdnCountryId").val(e.CountryId);
    $("#CountryId").val(e.CountryId);
    $("#hdnGovernotesId").val(e.GovernotesId);
    $("#hdnPlaceId").val(e.PlaceId);
    $("#hdnBlockId").val(e.BlockId);
    $("#hdnRequestedDate").val(e.RequestedDate);
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
    if (ServiceCode == "HAP") {
        $(".HAS").show();
        $(".RAS").hide();

    }
    else {
        $(".HAS").hide();
        $(".RAS").show();
    }
    $("#dvServiceDetail").show();
    if (e.ServiceEnrollmentStatus != 5) {

        $("#dvButton").show();
    }
    else {
        $("#dvButton").hide();
       
        
    }



    GetCustomerDetail(e.CPRNumber);
    
}

function OnChangingEnrollmentStatus(e) {
    if ($(e).val() != "" && $(e).val() != "0") {
        if ($(e).val() == "2") {
            $('.MEM').show();
            $('#liApRemarks').hide();
        }
        else {
            $('.MEM').hide();
            $('#liApRemarks').show();
        }
    }

    return true;
}

function GetCustomerDetail(CPRNumber) {
    if (CPRNumber != "") {
        var pUrl = "/Admin/ServiceRequest/BindCustomerDetail?CPRNumber=" + CPRNumber
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
                    $("#dvCustomerDetail").html(data);
                    $("#dvCustomerDetail").show();
                }

            }
        });
    }
}

function SearchServiceEnrollment() {

    var ServiceId = $("#ServiceId").val() == "" ? 0 : parseInt($("#ServiceId").val());
    var EnrollmentStatusId = $("#SearchServiceEnrollmentStatusId").val() == "" ? 0 : parseInt($("#SearchServiceEnrollmentStatusId").val());
    var CustomerName = $("#txtCustomerName").val();

    var pUrl = "/Admin/ServiceEnrollment/SearchServiceEnrollmentRequest?ServiceId=" + ServiceId + "&EnrollmentStatusId=" + EnrollmentStatusId + "&CustomerName=" + CustomerName
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
                $("#tblServiceRequest").html(data);
            }
        }
    });

}
