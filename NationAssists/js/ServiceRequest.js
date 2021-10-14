function BindServiceType() {
    if ($("#ServiceId").val() != "" && $("#ServiceId").val() != "0") {
        var pUrl = "/RaiseServiceRequest/BindServiceType?ServiceId=" + $("#ServiceId").val() ;
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {

                $("#ServiceCategoryId").html(""); // clear before appending new list
                var jData = JSON.parse(data);
                if (jData != null && jData.length > 0) {
                    $("#ServiceCategoryId").append($('<option></option>').val(0).html("--Select--"));
                    $.each(jData, function (i, service) {
                        $("#ServiceCategoryId").append(
                            $('<option></option>').val(service.ServiceSubCategoryId).html(service.Name));
                    });

                    var SelectedService = $("#ServiceId option:selected").text();
                    var ServiceCode = SelectedService.substring(SelectedService.indexOf('(') + 1, SelectedService.indexOf(')'));
                    if (ServiceCode == "HA") {
                        $('.RAP').hide();
                        $('.HAP').show();
                        $("#txtServiceLocation").hide();
                        $("#ddlServiceLocation").show();   

                    }
                    else {
                        $('.RAP').show();
                        $('.HAP').hide();
                        $("#txtServiceLocation").show();
                        $("#ddlServiceLocation").hide();  
                    }
                   // if (ServiceCode != "HAP") {
                        BindVehicleDetail($("#hdnCPRNumber").val(), $("#ServiceId").val());
                  //  }

                }
            }
        });
    }
}

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

                    $("#PlaceId").html($('<option></option>').val(0).html("--Select--"));
                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

                }
            }
        });
    }
}

function BindPlaces()
{
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

                    $("#BlockCode").html($('<option></option>').val(0).html("--Select--"));

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

                    

                }
            }
        });
    }
}

function SubmitServiceRequest() {

    if (ValidateForm()) {
        var objUsers = {
            ServiceRequestId: 0,
            CustomerId: $("#hdnCustomerId").val(),
            ServiceId: $("#ServiceId").val(),
            ServiceSubCategoryId: $("#ServiceCategoryId").val(),
            VehicleRegistrationNumber: $("#VehicleRegistrationNo").val(),
            ChessisNo: $("#ChessisNo").val(),
            ServiceLocation: $("#ServiceId").val() != "3" ? $("#txtServiceLocation").val() : $("#ddlServiceLocation").val(),
            CountryID: $("#CountryId").val(),
            GovernotesId: $("#GovernotesId").val(),
            PlaceId: $("#PlaceId").val(),
            LocationPinCode: $("#BlockCode").val(),
            DateOfAccident: $("#txtDateOfAccident").val(),
            NameOfWorkShop: $("#txtNameOfWorkshop").val(),
            BuildingNo: $("#txtBuildingNo").val(),
            CreatedBy: $("#hdnCustomerId").val(),
            StepiniCondtion: ($('input[type="radio"][name="StepiniCondtion"]:checked').val() == "NO" ? false : true),
            CollectRepairVehicleAddress: $("#txtCollectRepairVehicleAddress").val(),
            ContactMobileNo: $("#txtContactNo").val(),
            ReleventDetails: $("#txtReleventDetails").val(),
            Remarks: $("#txtRemarks").val(),
            SubCategoryName: $("#ServiceCategoryId option:selected").text(),
            IsRequestedFromCustomer:true

        };
        var pUrl = "/RaiseServiceRequest/SubmitServiceRequest/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objUsers),
            success: function (data) {
                debugger;

                if (data.Result && data.TicketNo != null) {
                    alert("Thanks for raising Service Request. Your Ticket No is " + data.TicketNo + ".");
                    window.location.href = "/Home";
                }
                else {
                    if (data.DuplicateEmail) {
                        alert("Email already exists.");
                    }
                    else if (data.DuplicateMobile) {
                        alert("Mobile No already exists.");
                    }
                    else {
                        alert("Opps! some error occured.");
                    }
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
  
    if ($('#ServiceId').val() == "" || $('#ServiceId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service. \n";
    }

    if ($('#ServiceCategoryId').val() == "" || $('#ServiceCategoryId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service Type. \n";
    }
   
   

    if ($('#CountryId').val() == "" || $('#CountryId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Country. \n";
    }

    if ($('#CountryId').val() != "" && $('#GovernotesId').val()=="0" ) {
        IsValid = false;
        strErrMsg += "Please select Governotes. \n";
    }

    if ($('#GovernotesId').val() != "0" && $('#PlaceId').val()=="0" ) {
        IsValid = false;
        strErrMsg += "Please select Place. \n";
    }
    if ($('#PlaceId').val() != "0" && $('#BlockCode').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Block. \n";
    }
    if ($('#txtDateOfAccident').val() == "") {
        IsValid = false;
        strErrMsg += "Please select Date Of Accident. \n";
    }

    if ($("#ServiceId").val() != "0") {
        var SelectedService = $("#ServiceId option:selected").text();
        var ServiceCode = SelectedService.substring(SelectedService.indexOf('(') + 1, SelectedService.indexOf(')'));
        if (ServiceCode == "HA") {

            if ($("#ddlServiceLocation").val() == "0" || $("#ddlServiceLocation").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Risk Address . \n";
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

            if ($("#txtPolicyType").val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Policy Type . \n";
            }
            if ($("#VehicleRegistrationNo").val() == "" || $("#VehicleRegistrationNo").val() == "0") {
                IsValid = false;
                strErrMsg += "Please select vehicle registration No. \n";
            }
            if ($("#ChessisNo").val() == "" || $("#ChessisNo").val() == "0") {
                IsValid = false;
                strErrMsg += "Please select Chassis No. \n";
            }
            if ($("#txtVehicleYear").val() == "" || $("#txtVehicleYear").val() == "0") {
                IsValid = false;
                strErrMsg += "Please enter Vehicle year . \n";
            }
            if ($('#txtServiceLocation').val() == "") {
                IsValid = false;
                strErrMsg += "Please enter Service Location. \n";
            }
        }
    }



    if ($("#txtContactNo").val() != "" && $("#txtContactNo").val().length < 8) {
        IsValid = false;
        strErrMsg += "Mobile No must contain 8 digit. \n";
    }
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }
}

function ShowServiceRequestDetail(e) {
    var hhh = "";
    $("#hdnServiceRequestId").val(e.ServiceRequestId);
    $("#txtServiceName").val(e.ServiceName);
    $("#txtServiceType").val(e.SubCategoryName);
    $("#txtVehicleRegistrationNumber").val(e.VehicleRegistrationNumber);
    $("#txtChessisNo").val(e.ChessisNo);
    $("#ServiceLocation").val(e.ServiceLocation);
    $("#txtCountryName").val(e.CountryName);
    $("#txtGovernotesName").val(e.GovernoratesName);
    $("#txtPlaceName").val(e.Place_Name);
    $("#txtBlockCode").val(e.Block_Code);
    $("#DateOfAccident").val(e.DateOfAccident);
    $("#NameOfWorkshop").val(e.NameOfWorkShop);
    $("#BuildingNo").val(e.BuildingNo);
    $("input[type=radio][name=SC][value=" + e.StepiniCondtion + "]").prop("checked", true);

    $("#CollectRepairVehicleAddress").val(e.CollectRepairVehicleAddress);
    $("#ContactNo").val(e.ContactMobileNo);
    $("#ReleventDetails").val(e.ReleventDetails);
    $("#dvServiceRequestDetail").show();
    $("#hdnServiceId").val(e.ServiceId);
    $("#hdnSubCategoryId").val(e.ServiceSubCategoryId);

    var SelectedService = e.ServiceName;
    var ServiceCode = SelectedService.substring(SelectedService.indexOf('(') + 1, SelectedService.indexOf(')'));
    if (ServiceCode == "HA") {
        $('.RAP').hide();
        $('.HAP').show();

    }
    else {
        $('.RAP').show();
        $('.HAP').hide();
    }
    var scrollPos = $("#dvServiceRequestDetail").offset().top;
    $(window).scrollTop(scrollPos);

    $("#dvRaiseServiceRequest").hide();

    //dvDetail
}

function BindVehicleDetail(CPRNumber, ServiceId) {
    var pUrl = "/RaiseServiceRequest/BindVehicleDetailByCPRNumber?CPRNumber=" + CPRNumber + "&ServiceId=" + ServiceId;
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        async: false,
        success: function (data) {

            $("#VehicleRegistrationNo").html(""); // clear before appending new list
            $("#ChessisNo").html("");
            $("#ddlServiceLocation").html("");
            var jData = JSON.parse(data);
            if (jData != null ) {
                $("#VehicleRegistrationNo").append($('<option></option>').val(0).html("--Select--"));
                $("#ChessisNo").append($('<option></option>').val(0).html("--Select--"));
                $.each(jData.ChessisList, function (i, Chessis) {
                    $("#ChessisNo").append(
                        $('<option></option>').val(Chessis).html(Chessis));
                });
                $.each(jData.VehicleRegistrationNoList, function (i, VehicleRegistrationNo) {
                    $("#VehicleRegistrationNo").append(
                        $('<option></option>').val(VehicleRegistrationNo).html(VehicleRegistrationNo));
                });
                $.each(jData.RiskAddresses, function (i, rd) {
                    $("#ddlServiceLocation").append(
                        $('<option></option>').val(rd).html(rd));
                });

                

            }
        }
    });
}