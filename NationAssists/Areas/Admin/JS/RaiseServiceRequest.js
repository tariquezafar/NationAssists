function BindCustomerStatus() {
    if ($("#txtCPRNumber").val() != "") {

        var pUrl = "/Admin/ServiceRequest/SearchCustomerStatus?CPRNumber=" + $("#txtCPRNumber").val();
        $.ajax({
            type: "Get",
            url: pUrl,
            data: {},
            dataType: 'html',
            contentType: false,
            processData: false,
            async: false,
            success: function (data) {
                
                $('#tblCustomerStatus').html('');
                var jData = JSON.parse(data);
               
                if (jData != null && jData.length > 0) {
                    var table = "<table> <thead> <tr>  <th>SNo.</th> <th>Customer Type</th> <th>Is Having Membership</th> <th>Membership Status</th><th>Source</th> <th>Service Name</th><th>Membership Effective Date</th> <th>Membership Expiry Date</th>  <th>Is Signup Completed</th> <th>Customer Detail </th> <th>Action </th>  </tr> </thead>";

                    table += "<tbody>";
                    jData.forEach(function (e, i) {
                        var tbody = "<tr>";
                     
                        tbody += "<td>" + (i + 1) + "</td>";
                        tbody += "<td>" + (e.CustomerType != null ? (e.CustomerType == "NA" ? "Nation Assist Member" : "Guest Customer") : (e.IsHavingMembership ? "Nation Assist Member" :"NA")) + "  </td>";
                        tbody += "<td>" + (e.IsHavingMembership ==true ? "Yes":"No") + " </td>";
                        tbody += "<td>" + (e.IsMemberShipExpired == true ? "In Active" : "Active") + "</td>";
                        tbody += "<td>" + e.SourceName + "</td>";
                        tbody += "<td>" + e.ServiceName + "</td>";
                        tbody += "<td>" + (e.EffectiveDate == null ? '' : e.EffectiveDate)+ "</td>";
                        tbody += "<td>" + (e.ExpiryDate== null ? '' :e.ExpiryDate) + "</td>";
                       
                        tbody += "<td>" + (e.IsSignUpCompleted == true ? "Yes" : "No") + " </td>";
                        tbody += "<td> " + (e.IsSignUpCompleted ? "<a style='cursor:pointer;color:green;' onclick='GetCustomerDetail()' >Customer Detail</a>" : "") + " </td>";
                        tbody += "<td> " + (e.IsHavingMembership && !e.IsMemberShipExpired  ? "<a style='cursor:pointer;color:green;' onclick='AddServiceRequest(" + JSON.stringify(e) + ")' >Add Service Request</a>" : "") +   " </td>";


                        tbody += "</tr>";

                        table += tbody;

                    });

                    $('#tblCustomerStatus').html(table);
                }
                else {
                    $('#tblCustomerStatus').html('');
                }
            }
        });
    }
    else {
        alert("Please enter CPR Number.");
        return false;
    }
}


function GetCustomerDetail() {
    if ($("#txtCPRNumber").val() != "") {
        var pUrl = "/Admin/ServiceRequest/BindCustomerDetail?CPRNumber=" + $("#txtCPRNumber").val()
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
function AddServiceRequest(e) {

    
    $("#hdnMembershipId").val(e.MembershipId);
    $("#hdnCustomerId").val(e.CustomerId);
    $("#hdnCustomerName").val(e.CustomerName);
    //e.SourceName.substring(0,e.SourceName.lastIndexOf("("))
    $("#hdnSourceName").val(e.SourceName.substring(0, e.SourceName.lastIndexOf("(")));
    $("#hdnCustomerEmail").val(e.CustomerEmail);
    $("#hdnIsSignUpCompleted").val(e.IsSignUpCompleted);
    var pUrl = "/Admin/ServiceRequest/BindServiceByCPRNumber?CPRNumber=" + $("#txtCPRNumber").val() + "&MembershipId=" + parseInt(e.MembershipId);
    $.ajax({
        type: "Get",
        url: pUrl,
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,
        
        success: function (data) {

            $('#ServiceId').html('');
            var jData = JSON.parse(data);

            if (jData != null && jData.length > 0) {
             

                $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
                $.each(jData, function (i, service) {
                    $("#ServiceId").append(
                        $('<option></option>').val(service.ServiceId).html(service.ServiceName + '(' + service.ServiceCode+')'));
                });

                $("#dvRaiseServiceRequest").show();
                $("#dvSearch").hide();
            }
            else {
                $("#ServiceId").append($('<option></option>').val(0).html("--Select Services--"));
            }
        }
    });


 
    $.ajax({
        type: "Get",
        url: "/Admin/ServiceRequest/BindChessisByCPRNumber?CPRNumber=" + $("#txtCPRNumber").val(),
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,

        success: function (data) {

            $('#ChessisNo').html('');
            var jData = JSON.parse(data);

            if (jData != null && jData.length > 0) {


                $("#ChessisNo").append($('<option></option>').val(0).html("--Select--"));
                $.each(jData, function (i, service) {
                    $("#ChessisNo").append(
                        $('<option></option>').val(service).html(service));
                });

                $("#dvRaiseServiceRequest").show();
                $("#dvSearch").hide();
            }
            else {
                $("#ChessisNo").append($('<option></option>').val(0).html("--Select--"));
            }
        }
    });


    $.ajax({
        type: "Get",
        url: "/Admin/ServiceRequest/BindVehicleRegistrationNoByCPRNumber?CPRNumber=" + $("#txtCPRNumber").val(),
        data: {},
        dataType: 'html',
        contentType: false,
        processData: false,

        success: function (data) {

            $('#VehicleRegistrationNo').html('');
            var jData = JSON.parse(data);

            if (jData != null && jData.length > 0) {


                $("#VehicleRegistrationNo").append($('<option></option>').val(0).html("--Select--"));
                $.each(jData, function (i, service) {
                    $("#VehicleRegistrationNo").append(
                        $('<option></option>').val(service).html(service));
                });

                $("#dvRaiseServiceRequest").show();
                $("#dvSearch").hide();
            }
            else {
                $("#VehicleRegistrationNo").append($('<option></option>').val(0).html("--Select--"));
            }
        }
    });
        
}

function BindServiceType() {
    if ($("#ServiceId").val() != "" && $("#ServiceId").val() != "0") {
        var pUrl = "../../../RaiseServiceRequest/BindServiceType?ServiceId=" + $("#ServiceId").val();
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
                    BindVehicleDetail($("#txtCPRNumber").val(), $("#ServiceId").val(), $("#hdnMembershipId").val() )
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
        var pUrl = "../../../RaiseServiceRequest/BindBlocks?PlaceId=" + $("#PlaceId").val();
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
            Customer_Name: $("#hdnCustomerName").val(),
            SourceName: $("#hdnSourceName").val(),
            EmailId: $("#hdnCustomerEmail").val(),
            IsRequestedFromCustomer: false,
            MembershipId: $("#hdnMembershipId").val()


        };
        var pUrl = "../../RaiseServiceRequest/SubmitServiceRequest/";
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
                    window.location.href = "/admin/ServiceRequest/AllocateServiceRequest";
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

    if ($('#ServiceId').val() == "" || $('#ServiceId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service. \n";
    }

    if ($('#ServiceCategoryId').val() == "" || $('#ServiceCategoryId').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Service Type. \n";
    }

    //if ($('#txtServiceLocation').val() == "") {
    //    IsValid = false;
    //    strErrMsg += "Please enter Service Location. \n";
    //}

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
                strErrMsg += "Please select Risk Address . \n";
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
            if ($("#VehicleRegistrationNo").val() == "") {
                IsValid = false;
                strErrMsg += "Please select vehicle registration No. \n";
            }
            if ($("#ChessisNo").val() == "") {
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

    if ( $("#hdnIsSignUpCompleted").val() == "false" && $("#txtContactNo").val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Contact No. \n";
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


function ComplateSignUp(e) {

    $('#txtSourceType').val(e.SourceType);
    $('#txtSource').val(e.SourceName);
    $('#hdnSourceId').val(e.SourceId);
    $("#hdnSourceTypeCode").val(e.SourceTypeCode);
    $('#dvCompleteSignup').show();
    $('#dvSearch').hide();

}

function SaveCustomer() {
    if (ValidateCustomerSignUp()) {
        var objUsers = {
            CustomerId: 0,
            FirstName: $("#txtFirstName").val(),
            LastName: $("#txtLastName").val(),
            EmailId: $("#txtEmailId").val(),
            MobileNo: $("#txtMobileNo").val(),
            NationalId: $("#txtCPRNumber").val(),
            Gender: $("#ddlGender").val(),
            BrokerId: $("#hdnSourceId").val(),
            AccountType: "NA",
            AccountSubType: $("#hdnSourceTypeCode").val(),
            Password: $("#txtCPRNumber").val(),
            Customer_Reference_Code: $("#txtSource").val().substring($("#txtSource").val().indexOf('(') + 1, $("#txtSource").val().indexOf(')')),
            IsCustomerCreatedFromCRM:true
        };
        var pUrl = "../../Register/SaveCustomer/";
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            url: pUrl,
            data: JSON.stringify(objUsers),
            success: function (data) {
                debugger;

                if (data.Result) {
                    alert(" Customer Registration is completed");
                    window.location.href = "/Admin/ServiceRequest/RaiseServiceRequest";
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


function ValidateCustomerSignUp() {
    var IsValid = true;
    var strErrMsg = "";

    if ($('#txtFirstName').val() == "" ) {
        IsValid = false;
        strErrMsg += "Please enter First Name. \n";
    }

    if ($('#ddlGender').val() == "" || $('#ddlGender').val() == "0") {
        IsValid = false;
        strErrMsg += "Please select Gender. \n";
    }

    if ($('#txtEmailId').val() == "") {
        IsValid = false;
        strErrMsg += "Please enter Email-Id. \n";
    }

    if ($('#txtMobileNo').val() == "" ) {
        IsValid = false;
        strErrMsg += "Please enter Mobile No. \n";
    }
    if (IsValid) {
        return true;
    }
    else {
        IsValid = false;
        alert(strErrMsg);
    }

}

function BindVehicleDetail(CPRNumber, ServiceId, MembershipId) {
    var pUrl = "/RaiseServiceRequest/BindVehicleDetailByCPRNumber?CPRNumber=" + CPRNumber + "&ServiceId=" + ServiceId + "&MembershipId=" + MembershipId;
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
            $("#ChessisNo").html("")
            $("#ddlServiceLocation").html("")
            var jData = JSON.parse(data);
            if (jData != null) {
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